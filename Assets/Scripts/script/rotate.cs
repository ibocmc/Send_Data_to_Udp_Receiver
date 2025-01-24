using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	public float MaxTiltAngle = 20.0f;    // The maximum angle the board can tilt
	public float tiltSpeed = 30.0f;       // tilting speed in degrees/second
	Vector3 curRot;
	float maxZ;
	float minZ;




	// Use this for initialization
	void Start () {

		// Get initial rotation
		curRot = this.transform.eulerAngles;
		// calculate limit angles:
		maxZ = curRot.z + MaxTiltAngle;
		minZ = curRot.z - MaxTiltAngle;

		
	}
	
	// Update is called once per frame
	void Update () {

		// "rotate" the angles mathematically:
		curRot.z += Input.GetAxis("Horizontal") * Time.deltaTime * tiltSpeed;
		// Restrict rotation along z axes to the limit angles:
		curRot.z = Mathf.Clamp(curRot.z, minZ, maxZ);

		// Set the object rotation
		this.transform.eulerAngles = curRot;

		
	}
}
