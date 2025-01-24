using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hareket : MonoBehaviour {


	public float speed = 20.0f;
	public float smooth = 2.0f;
	public float tiltAngle = 30.0f;
	public float ySpeed = 2.0f;
	private float yRotation = 0f;

	private float h;
	private float v;

	public float Current_speed;

	public float brakeForce;
	public float thrustForce;

	public float roll;
	public float pitch;
	public float yaw;
	public float forceAcc;
	private Rigidbody rb;

	public float movex;

	public float smoothTime = 3f; 
	


	private void Start()
    {
		rb = GetComponent<Rigidbody>();
	}

    private void Update()
    {
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		float acc = Input.GetAxis("Fire1") * 10;
		float brk = Input.GetAxis("Fire2") * 10;


		speed += acc*Time.deltaTime;
        speed -= brk*Time.deltaTime;

		//if (speed > 10)
		//{
		//	speed = 0;
		//}

  //      if (speed < -10)
  //      {
		//	speed = 0;
  //      }

		Current_speed = rb.velocity.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate () {



		//transform.Translate(speed * Time.fixedDeltaTime * Vector3.forward);

		float tiltAroundZ = h * tiltAngle;
		float tiltAroundX = -v * tiltAngle;

        yRotation += Input.GetAxis("Horizontal") * ySpeed * Time.deltaTime;
        Quaternion target = Quaternion.Euler(tiltAroundX, yRotation, tiltAroundZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        //rb.AddTorque(Vector3.up * tiltAroundZ);// yaw
        //rb.AddTorque(Vector3.forward * tiltAroundZ ); // roll
        //rb.AddTorque(Vector3.right * tiltAroundX);
        //rb.AddForce(Vector3.left * tiltAroundZ);


        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
		forceAcc = rb.GetAccumulatedForce(1).magnitude;
		
		//if (Input.GetKey(KeyCode.Mouse0)) // Move forward
		//{
		//	targetPosition += speed * Time.fixedDeltaTime * transform.forward;
		//}
		

		//// Smoothly move towards the target position using Lerp
		//transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / smoothTime);


		//float yawInput = 0;
		//if (Input.GetKey(KeyCode.Q)) yawInput = -1f; // Yaw left
		//if (Input.GetKey(KeyCode.E)) yawInput = 1f; // Yaw right

		//steerAmountY = Mathf.Clamp(yawInput, -1, 1);

		//steerRotY = Mathf.Lerp(steerRotY, steerAmountY * 60 , Time.deltaTime*1.25f);

		//transform.localEulerAngles = new Vector3(0, steerRotY, 0);


		//update the position
		transform.position = transform.position + new Vector3(h * movex * Time.deltaTime * smooth, /*v * -movex * Time.deltaTime*/0, 0);


		roll = transform.localEulerAngles.z ;
		roll = (roll > 180) ? roll - 360 : roll;

		pitch = transform.localEulerAngles.x ;
		pitch = (pitch > 180) ? pitch - 360 : pitch;

		yaw = transform.localEulerAngles.y;
		yaw = (yaw > 180) ? yaw - 360 : yaw;


	}
}
