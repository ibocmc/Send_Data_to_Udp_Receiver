using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlHover : MonoBehaviour {

	public float hoverHeight;
	public float thrustForce;
	public float steerForce;
	public float steerPosZ;
	public float dumping;
	public float driveForce;
//	public float TurnTiltForce;
//	public float ForwardTiltForce;

//	public float MAX_TILT;
//
//	public float rotForce;


	public float smooth;
	public float tiltAngle;


	public Rigidbody hvrb;

	public Vector3[] thrusters;

//	private Vector3 hMove = Vector3.zero;
//	private Vector3 hTilt = Vector3.zero;
//	private float hTurn = 0f;



	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate(){

		RaycastHit hit;
		int i;

		for(i = 0; i < thrusters.Length ; i++ ){

			Vector3 wdThrusters = transform.TransformPoint(thrusters[i]);

			if(Physics.Raycast(wdThrusters,-transform.up,out hit)){


				float discrep = hoverHeight - hit.distance;
				float upVel = hvrb.GetRelativePointVelocity(wdThrusters).y;
				hvrb.AddForceAtPosition(transform.up * (thrustForce * discrep - upVel * dumping),wdThrusters);

			}
		}

		float fwd = -Input.GetAxis("Vertical");
		hvrb.AddForce (transform.forward * (fwd * driveForce));
//		if(Input.GetKey(KeyCode.RightArrow)){
//		TiltProcess ();
//		}

		float steer = -Input.GetAxis("Horizontal");
		hvrb.AddForceAtPosition (transform.right * (steerForce * steer), transform.TransformPoint (Vector3.forward * - steerPosZ));




//		transform.Translate(Vector3.forward * Time.deltaTime * -driveForce);

		//float tiltAroundZ = Input.GetAxis ("Horizontal") * tiltAngle;
		float tiltAroundX = -Input.GetAxis ("Vertical") * tiltAngle;
		float tiltAroundY = -Input.GetAxis ("Horizontal") * tiltAngle;

		Quaternion target = Quaternion.Euler (tiltAroundX, tiltAroundY,0);

		transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * smooth);



//			if(Input.GetKey(KeyCode.RightArrow)){
//
//		hvrb.AddRelativeTorque(0f,0f,Mathf.Clamp(tiltAngle,-MAX_TILT,MAX_TILT));
////
////				hvrb.transform.Rotate (Vector3.up * Time.deltaTime * rotForce);
////				//hvrb.transform.Rotate (0, 0, tiltAngle* Time.deltaTime);
////
//			}
//
//			if(Input.GetKey(KeyCode.LeftArrow)){
//
//
//				hvrb.transform.Rotate (Vector3.down * Time.deltaTime * rotForce);
//
//			}
//


	}

//	private void TiltProcess()
//	{
//		hTilt.x = Mathf.Lerp(hTilt.x, hMove.x * TurnTiltForce, Time.deltaTime);
//		hTilt.y = Mathf.Lerp(hTilt.y, hMove.y * ForwardTiltForce, Time.deltaTime);
//		hvrb.transform.localRotation = Quaternion.Euler(hTilt.y, hvrb.transform.localEulerAngles.y, -hTilt.x);
//	}


//	public void OnDrawGizmos(){
//		int i;
//		for (i = 0; i < thrusters.Length; i++) {
//			Gizmos.DrawWireSphere(transform.TransformPoint(thrusters[i]), 0.1);
//		}
//	}


	
	// Update is called once per frame
	void Update () {


	}
}

