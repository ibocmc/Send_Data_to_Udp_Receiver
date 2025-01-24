using UnityEngine;
using System.Collections;

public class tnkrcktrotate : MonoBehaviour {



	private float xRot = 0;


	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.DownArrow)){


			xRot += 1;
			if (xRot > 35)
				xRot = 35;

			//transform.Rotate( xRot,0f,0f);

			transform.rotation = Quaternion.Euler (xRot, 0, 0);


			//transform.Rotate(Time.deltaTime * 20f,0f,0f);


		}

		if (Input.GetKey (KeyCode.UpArrow)) {

			//transform.Rotate(-Time.deltaTime * 20f,0f,0f);


			xRot -= 1;
			if (xRot < -5)
				xRot = -5;

			transform.rotation = Quaternion.Euler (xRot, 0, 0);




		}


	}
}
