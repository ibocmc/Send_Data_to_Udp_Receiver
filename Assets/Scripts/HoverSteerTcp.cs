using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System;



// Class for steering hover vehicles
    public class HoverSteerTcp : MonoBehaviour
    {

        private TcpClient client;
        private NetworkStream stream;
       
        public string ipAddress = "127.0.0.1";
        public int port = 12345;


        public string pitch;

        public string roll;

        public string yaw;

        Transform tr;
        public Rigidbody vp;
        public float steerRate = 1;

        float steerAmountX;

        float steerAmountY;

        float steerAmountZ;

        [Tooltip("Curve for limiting steer range based on speed, x-axis = speed, y-axis = multiplier")]
        public AnimationCurve steerCurve = AnimationCurve.Linear(0, 1, 30, 0.1f);

        [Tooltip("Horizontal stretch of the steer curve")]
        public float steerCurveStretch = 1;
      

        [Header("Visual")]

        public bool rotate;
        public float maxDegreesRotation=30;
        public float rotationOffset;
        float steerRotX;
        float steerRotY;
        float steerRotZ;

        void Start() {

            maxDegreesRotation = 30;

            tr = transform;

            vp = GetComponent<Rigidbody>();

            ConnectToServer();
        }

    private void ConnectToServer()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 12345);
            stream = client.GetStream();
            //Debug.Log("Connected to server.");
        }
        catch (SocketException e)
        {
            Debug.LogError($"SocketException: {e.Message}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not connect to server: {e.Message}");
        }
    }

   

        void FixedUpdate() {
            // Set steering of hover wheels
            float rbSpeedX = vp.velocity.x / steerCurveStretch;
            float steerLimitX = steerCurve.Evaluate(Mathf.Abs(rbSpeedX));

            float rbSpeedY = vp.velocity.y / steerCurveStretch;
            float steerLimitY = steerCurve.Evaluate(Mathf.Abs(rbSpeedY));

            float rbSpeedZ = vp.velocity.z / steerCurveStretch;
            float steerLimitZ = steerCurve.Evaluate(Mathf.Abs(rbSpeedZ));

            steerAmountZ = -Input.GetAxis("Horizontal") * steerLimitZ;
            steerAmountX = Input.GetAxis("Vertical") * steerLimitX;

            float yawInput = 0;
            if (Input.GetKey(KeyCode.Q)) yawInput = -1f; // Yaw left
            if (Input.GetKey(KeyCode.E)) yawInput = 1f; // Yaw right

            steerAmountY = Mathf.Clamp(yawInput, -1, 1);

        }

        void Update() {
            // Set visual rotation
            if (rotate) {
                steerRotX = Mathf.Lerp(steerRotX, steerAmountX * maxDegreesRotation + rotationOffset, steerRate * 0.1f * Time.timeScale);
                steerRotZ = Mathf.Lerp(steerRotZ, steerAmountZ * maxDegreesRotation + rotationOffset, steerRate * 0.1f * Time.timeScale);
                steerRotY = Mathf.Lerp(steerRotY, steerAmountY * maxDegreesRotation + rotationOffset, steerRate * Time.deltaTime * 2.5f);

                // tr.localEulerAngles = new Vector3(tr.localEulerAngles.x, tr.localEulerAngles.y, steerRot);

                tr.localEulerAngles = new Vector3(steerRotX, steerRotY, steerRotZ);

                pitch = steerRotX.ToString("f2");
                roll = steerRotZ.ToString("f2");
                yaw = steerRotY.ToString("f2");

                if (client != null && client.Connected)
                {
           
                    SendRotationData();
                }

            }
        }

    private void SendRotationData()
    {
       
        string rotationData = $"X:{pitch} Y:{yaw} Z:{roll}";
       
        byte[] data = Encoding.ASCII.GetBytes(rotationData);

        try
        {
            if (stream != null && stream.CanWrite)
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
                //Debug.Log("Data sent to server: " + rotationData);
            }
            else
            {
                Debug.LogError("Stream is not writable.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error sending data: {e.Message}");
        }
    }


        private void OnApplicationQuit()
        {
            stream?.Close();
            client?.Close();
        }

    }

