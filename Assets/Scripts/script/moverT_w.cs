using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverT_w : MonoBehaviour {

	public Rigidbody twrb;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		twrb.AddRelativeForce (Vector3.forward * speed, ForceMode.Acceleration);
		//twrb.velocity = new Vector3( 0 , 0, speed);
		
	}
}
