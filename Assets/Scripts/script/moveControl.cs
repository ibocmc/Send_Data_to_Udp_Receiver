using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]


public class axleLoad{

	public WheelCollider left_wheel;
	//public WheelCollider leftRear_wheel1;
	//public WheelCollider leftRear_wheel2;

	public WheelCollider right_wheel;
	//public WheelCollider rightRear_wheel1;
	//public WheelCollider rightRear_wheel2;
	 


	public GameObject left_wheel_M;
	//public GameObject leftRear_wheel1_M;
	//public GameObject leftRear_wheel2_M;

	public GameObject right_wheel_M;
	//public GameObject rightRear_wheel1_M;
	//public GameObject rightRear_wheel2_M;

	public bool motor;  // is the wheel attached to motor
	public bool steering;  // does this wheel apply steer angle

	public GameObject axe;

	public bool brake;
}




public class moveControl : MonoBehaviour {



	//public bool motor;  // is the wheel attached to motor
	//public bool steering;  // does this wheel apply steer angle



	public float maxMotorTorque;  // max torque the motor can apply the to the wheels 
	public float maxSteeringAngle;  // max steering angel of the wheels
	public float Brake;
	public float maxBrake;
	public float brkAcc;

	public float topSpeed = 100; // km per hour
	public float currentSpeed = 0;
	public float Pitch;

	public AudioSource TruckEngine;
	//public AudioSource T_brake;




	public Rigidbody rb;

	public Transform centerofmass;

	public List <axleLoad> axleInfos; // list of all wheel pairs

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody> ();
		rb.centerOfMass = centerofmass.localPosition;

		TruckEngine.Play ();

		//maxBrake = 0;
	
	}
	
	// Update is called once per frame
	void Update () {





		if (Input.GetKey (KeyCode.DownArrow)) {


			//T_brake.Play ();
			Brake += brkAcc;


			if(Brake >= maxBrake)
			{

				Brake = maxBrake;

			}

		} 

		if(Input.GetKeyUp(KeyCode.DownArrow)){
			
				Brake = 0;
				//t_brake.Stop ();
		
		}


	
	}

	public void ApplyLocalPositionToVisuals(axleLoad wheelPair){

		//wheelPair.left_wheel_M.transform.Rotate (Vector3.right, Time.deltaTime * wheelPair.left_wheel.rpm * 10, Space.Self);
		//wheelPair.right_wheel_M.transform.Rotate (Vector3.left, Time.deltaTime * wheelPair.right_wheel.rpm , Space.Self);


	}


	public void Updatepos(axleLoad wcolls){

		Quaternion quat;
		Vector3 pos;
		Quaternion quat1;
		Vector3 pos1;

		wcolls.left_wheel.GetWorldPose (out pos, out quat);
		wcolls.right_wheel.GetWorldPose (out pos1, out quat1);

		wcolls.left_wheel_M.transform.position = pos;
		wcolls.left_wheel_M.transform.rotation = quat;

		wcolls.right_wheel_M.transform.position = pos1;
		wcolls.right_wheel_M.transform.rotation = quat1;

		wcolls.axe.transform.position = pos;

	}


//	public void TurnWheel(axleLoad wheels){
//
//		//float turning = maxSteeringAngle * Input.GetAxis("Horizontal");
//
//		wheels.left_wheel_M.transform.Rotate (Vector3.up, 0, 0);
//
//	}


	public void FixedUpdate(){

		float motor = maxMotorTorque * Input.GetAxis ("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

		currentSpeed = transform.GetComponent <Rigidbody> ().velocity.magnitude * 3.6f;
		Pitch = currentSpeed / topSpeed;

		if (Input.GetKey (KeyCode.UpArrow)) {

			TruckEngine.pitch = Pitch;
			//TruckEngine.Play ();



		} 

		if(Input.GetKey(KeyCode.DownArrow))
		
		{
			
			TruckEngine.pitch = Pitch;
			//TruckEngine.Play ();
		}





		//TruckEngine.pitch = Mathf.Clamp(currentSpeed / 20, 0, 1.2f);
//		
//		TruckEngine.pitch = Pitch;
		//TruckEngine.Play();

		//transform.GetComponent <AudioSource> ().Pitch = pitch;



		foreach(axleLoad axleInfo in axleInfos ){

			// check steering




			if(axleInfo.steering == true && axleInfo.brake == true){

				axleInfo.left_wheel.steerAngle = steering;
				axleInfo.right_wheel.steerAngle = steering;

			//	axleInfo.left_wheel_M.transform.localEulerAngles = new Vector3 (axleInfo.left_wheel_M.transform.localEulerAngles.x, steering - axleInfo.left_wheel_M.transform.localEulerAngles.z, axleInfo.left_wheel_M.transform.localEulerAngles.z);
			//	axleInfo.right_wheel_M.transform.localEulerAngles = new Vector3 (axleInfo.right_wheel_M.transform.localEulerAngles.x, steering - axleInfo.right_wheel_M.transform.localEulerAngles.z - 180 , axleInfo.right_wheel_M.transform.localEulerAngles.z);
			}


			// check the motor

			if(axleInfo.motor == true ){

				axleInfo.left_wheel.motorTorque = motor;
				axleInfo.right_wheel.motorTorque = motor;
				//TruckEngine.Play ();



			}



			if(axleInfo.brake == true){

				axleInfo.left_wheel.brakeTorque = Brake;
				axleInfo.right_wheel.brakeTorque = Brake;


			}


			if (Input.GetKey (KeyCode.DownArrow)) {

				axleInfo.left_wheel.motorTorque = 0;
				axleInfo.right_wheel.motorTorque = 0;
				Brake += brkAcc;


				if(Brake >= maxBrake)
				{

					Brake = maxBrake;

				}

			} 

			if(Input.GetKeyUp(KeyCode.DownArrow)){

				Brake = 0;
				axleInfo.left_wheel.motorTorque = motor;
				axleInfo.right_wheel.motorTorque = motor;
			}


			
			ApplyLocalPositionToVisuals (axleInfo);
			Updatepos (axleInfo);

			//TurnWheel (axleInfo);




		}


	}




}
