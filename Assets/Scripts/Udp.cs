using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Text;



// Class for steering hover vehicles
public class Udp : MonoBehaviour
{

    private UdpClient udpClient;
    public string ipAddress = "127.0.0.1"; // Change to your target IP
    public int port = 12345;

    public string pitch;

    public string roll;

    public string yaw;

    public string sway;

    public string surge;

    public string heave;

    public Hareket hrkt;


    void Start()
    {
      
        udpClient = new UdpClient();
        hrkt = GetComponent<Hareket>();
    }

    void Update()
    {

        SendData();
        
    }

    private void SendData()
    {

        pitch = hrkt.pitch.ToString("F2");
        yaw = hrkt.yaw.ToString("F2");
        roll = hrkt.roll.ToString("F2");

        sway = "0.00";
        surge = "0.00";
        heave = "0.00";



        string rotationData = $"X:{pitch} Y:{yaw} Z:{roll} A:{sway} B:{surge} C:{heave}";

        byte[] data = Encoding.ASCII.GetBytes(rotationData);
   
        udpClient.Send(data, data.Length, ipAddress, port);
    }

    void OnApplicationQuit()
    {
        udpClient?.Close();
    }



}

