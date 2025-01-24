using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RotationSender : MonoBehaviour
{
    private UdpClient udpClient;
    public string ipAddress = "127.0.0.1"; // Change to your target IP
    public int port = 12345;

    // Rotation speeds
    public float rollSpeed = 10f; // Speed of roll rotation
    public float pitchSpeed = 10f; // Speed of pitch rotation
    public float yawSpeed = 10f; // Speed of yaw rotation

    // Rotation limits in degrees
    public float maxPitch = 30f; // Maximum pitch angle
    public float minPitch = -30f; // Minimum pitch angle
    public float maxYaw = 45f; // Maximum yaw angle
    public float minYaw = -45f; // Minimum yaw angle
    public float maxRoll = 30f; // Maximum roll angle
    public float minRoll = -30f; // Minimum roll angle
   

    public string pitch;
    
    public string roll;
    
    public string yaw;
        
    

    void Start()
    {
        udpClient = new UdpClient();
    }

    void Update()
    {
        HandleInput();
        LimitRotation();
        //SendData();
      
       // Vector3 currentRotation = transform.rotation.eulerAngles;

        //string pitch ="X:"+currentRotation.x.ToString("f2");
        //string roll ="Z:"+currentRotation.z.ToString("f2");
        //string yaw ="Y:"+currentRotation.y.ToString("f2");

        SendData2(pitch);
        SendData2(roll);
        SendData2(yaw);
    }

    private void HandleInput()
    {
        float rollInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float pitchInput = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow
        float yawInput = 0f;

        if (Input.GetKey(KeyCode.Q)) yawInput = -1f; // Yaw left
        if (Input.GetKey(KeyCode.E)) yawInput = 1f; // Yaw right

        // Apply rotation based on input
        transform.Rotate(pitchInput * pitchSpeed * Time.deltaTime, yawInput * yawSpeed * Time.deltaTime, rollInput * -rollSpeed * Time.deltaTime);
    }

    private void LimitRotation()
    {
        // Get the current rotation in Euler angles
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Normalize angles to be within 0-360
        currentRotation.x = NormalizeAngle(currentRotation.x);
        currentRotation.y = NormalizeAngle(currentRotation.y);
        currentRotation.z = NormalizeAngle(currentRotation.z);

        if (currentRotation.x > 180) // Convert to -180 to 180 range for easier clamping
        {
            currentRotation.x -= 360;
        }
        if (currentRotation.x > maxPitch)
        {
            currentRotation.x = maxPitch; // Set to max if exceeded
        }
        else if (currentRotation.x < minPitch)
        {
            currentRotation.x = minPitch; // Set to min if below
        }



        // Limit yaw (y-axis)
        if (currentRotation.y > 180) // Convert to -180 to 180 range for easier clamping
        {
            currentRotation.y -= 360;
        }

        if (currentRotation.y > maxYaw)
        {
            currentRotation.y = maxYaw; // Set to max if exceeded
        }
        else if (currentRotation.y < minYaw)
        {
            currentRotation.y = minYaw; // Set to min if below
        }

        // Limit roll (z-axis)

        if (currentRotation.z > 180) // Convert to -180 to 180 range for easier clamping
        {
            currentRotation.z -= 360;
        }
        if (currentRotation.z > maxRoll)
        {
            currentRotation.z = maxRoll; // Set to max if exceeded
        }
        else if (currentRotation.z < minRoll)
        {
            currentRotation.z = minRoll; // Set to min if below
        }

        // Apply the clamped rotation back to the transform
        transform.rotation = Quaternion.Euler(currentRotation);

         pitch = "X:" + (transform.rotation.x*100).ToString("f2");
         roll = "Z:" + (transform.rotation.z*100).ToString("f2");
         yaw = "Y:" + (transform.rotation.y*100).ToString("f2");
    }

    private float NormalizeAngle(float angle)
    {
        // Normalize the angle to be within 0-360 degrees
        if (angle < 0) angle += 360;
        if (angle >= 360) angle -= 360;
        return angle;
    }

    private void SendData()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        //string message = $"{("X:"+currentRotation.x).ToString("f1")},{"Y:"+currentRotation.y},{"Z:"+currentRotation.z}";

        string pitch = currentRotation.x.ToString("f0");
        string roll = currentRotation.z.ToString("f0");
        string yaw = currentRotation.y.ToString("f0");

        string message = "X:" + pitch + "Y:" + yaw + "Z:" + roll;


        byte[] data= Encoding.UTF8.GetBytes(message);

        //byte[] data = Encoding.UTF8.GetBytes(message);


        udpClient.Send(data, data.Length, ipAddress, port);
       
    }

    private void SendData2(string message)
    {
        // Convert the message to bytes
        byte[] data = Encoding.UTF8.GetBytes(message);

        // Send the data to the specified IP and port
        udpClient.Send(data, data.Length, ipAddress, port);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}