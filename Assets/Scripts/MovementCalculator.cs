using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCalculator : MonoBehaviour
{
    public float maxThrustForce = 10f;
   
    public float lateralForce = 5f;  // Lateral force for sway
    public float torqueForce = 5f;    // Torque for rotation yaw

    public Vector3 velocity;
    public Vector3 angularVelocity;

    public Vector3 surge;
    public Vector3 sway;
    public Vector3 yaw;


    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();

    }

    
    void FixedUpdate()
    {
        // Get user input for thrust and lateral movement
        float thrustInput = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
        float lateralInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        // Apply thrust force (surge)
         surge = transform.forward * thrustInput * maxThrustForce;
        rb.AddForce(surge);

        // Apply lateral force (sway)
         sway = transform.right * lateralInput * lateralForce;
        rb.AddForce(sway);

        // Apply torque for rotation (roll and pitch)
         yaw = new Vector3(0, lateralInput * torqueForce, 0); // Yaw rotation
        rb.AddTorque(yaw);

        // Log the current velocity and angular velocity
         velocity = rb.velocity; // Current linear velocity
         angularVelocity = rb.angularVelocity; // Current angular velocity

        
    }

   
}
