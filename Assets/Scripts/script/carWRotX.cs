using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carWRotX : MonoBehaviour {

	private float xRot = 0;
	public float rSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		xRot = Input.GetAxis ("Mouse Y") * -rSpeed;
		transform.Rotate(xRot * Time.deltaTime,0,0);
		
	}
}
