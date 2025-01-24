using UnityEngine;
using System.Collections;

public class hover : MonoBehaviour {

	public float speed = 90f;
	public float tiltAngle= 0f;
	public float tiltForce = .001f;
	public float turnSpeed = 5f;
	public float hoverForce = 65f;
	public float hoverHeight = 3.5f;
	private float powerInput;
	private float turnInput;
	private const float MAX_TILT = .1f;
	private Rigidbody carRigidbody;


	void Awake () 
	{
		carRigidbody = GetComponent <Rigidbody>();
	}

	void Update () 
	{
		powerInput = - Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
		tiltAngle = turnInput * tiltForce;
	}


	void FixedUpdate()
	{
		Ray ray = new Ray (transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, hoverHeight))
		{
			float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
			Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
			carRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
		}

		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		carRigidbody.AddRelativeTorque(0f,0f,Mathf.Clamp(tiltAngle,-MAX_TILT,MAX_TILT));




	}
}