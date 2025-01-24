using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;



    // Class for steering hover vehicles
    public class HoverSteerUdp : MonoBehaviour
    {

        private UdpClient udpClient;
        public string ipAddress = "127.0.0.1"; // Change to your target IP
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
            tr = transform;
            vp = GetComponent<Rigidbody>();
            udpClient = new UdpClient();
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
           
            if (rotate) {
                steerRotX = Mathf.Lerp(steerRotX, steerAmountX * maxDegreesRotation + rotationOffset, steerRate * Time.deltaTime);
                steerRotZ = Mathf.Lerp(steerRotZ, steerAmountZ * maxDegreesRotation + rotationOffset, steerRate * Time.deltaTime);
                steerRotY = Mathf.Lerp(steerRotY, steerAmountY * maxDegreesRotation + rotationOffset, steerRate * Time.deltaTime * 2.5f);

               

                tr.localEulerAngles = new Vector3(steerRotX, steerRotY, steerRotZ);

                pitch = steerRotX.ToString("f2");
                roll = steerRotZ.ToString("f2");
                yaw = steerRotY.ToString("f2");

                SendData();
            }
        }

        private void SendData()
        {
            string rotationData = $"X:{pitch} Y:{yaw} Z:{roll}";

            byte[] data = Encoding.ASCII.GetBytes(rotationData);

            // Send the data to the specified IP and port
            udpClient.Send(data, data.Length, ipAddress, port);
        }

        void OnApplicationQuit()
        {
            udpClient?.Close();
        }
        
        

    }

