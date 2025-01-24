using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bRotate : MonoBehaviour {


	private float yRot = 0;
	public float rSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		yRot = Input.GetAxis ("Mouse X") * rSpeed;
		//+yRot = Input.GetAxis ("MouseY") * rSpeed;




//		if(Input.GetKey(KeyCode.LeftArrow)){
//
//
//			yRot += 1;
//			if (yRot > 60)
//				yRot = 60;
//
//			//transform.Rotate( xRot,0f,0f);
//
//		transform.rotation = Quaternion.Euler (0, yRot, 0);
//
//
		transform.Rotate(0,yRot * Time.deltaTime,0);
//
//
//		}
//
//		if (Input.GetKey (KeyCode.RightArrow)) {
//
//			//transform.Rotate(-Time.deltaTime * 20f,0f,0f);
//
//
//			yRot -= 1;
//			if (yRot < -60)
//				yRot = -60;
//
//			transform.rotation = Quaternion.Euler (0, yRot, 0);

		
	}
}

