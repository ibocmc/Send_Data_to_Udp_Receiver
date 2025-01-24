using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Look : MonoBehaviour
{

	
	public float moveSpeed = 100.0f;
	public float alphaValue = 1.0f;



	//public float Rotation_Speed;
	//public float Rotation_Friction; //The smaller the value, the more Friction there is. [Keep this at 1 unless you know what you are doing].
	public float Rotation_Smoothness; //Believe it or not, adjusting this before anything else is the best way to go.


	private Quaternion Quaternion_Rotate_From;
	private Quaternion Quaternion_Rotate_To;







	//public GameObject pointArea;

	//public Transform _Player;

	//public ParticleSystem speedEffect;
	

	//public float sensitivity;
	//public float SkydivingMass;

	public float rotx;

	public float rotz;

	//public float konumZ;

	public GameObject Player;

	public float MLx;
	public float MLy;
	public float MLz;

	public float Gravity = 10;

	//public Vector3 MaxVelocity;


	public bool GameStarted;
	public bool skydiving;


	public float tiltZ;
	public float tiltX;
	public float ySpeed;
	private float yRotation;


	public Rigidbody RB;


	public float ForwardForce = 10;

	//public float FF_denge;

	public float maxForce;

	public float hizartisi1;
	public float hizartisi2;
	public float hizartisi3;

	public float hizazalis1;
	public float hizazalis2;


	public float smooth;



	// Update is called once per frame
	void FixedUpdate()
	{

		

		if (GameStarted)
		{


			if (skydiving == false)
			{

				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.Euler(-90, 0, 0);


			}

			//Pitch();
			//Roll();

			Konumlama();
			Rotate();
		






			float tiltAroundZ = Input.GetAxis("Horizontal");
			float tiltAroundX = -Input.GetAxis("Vertical");
			yRotation = Input.GetAxis("Horizontal") * ySpeed;

			//yRotation = MLy * ySpeed;

			//yRotation = MLz * ySpeed;

			MLx = Mathf.Clamp(tiltAroundX, -10, 10);
			MLy = Mathf.Clamp(yRotation, -30, 30);
			MLz = Mathf.Clamp(tiltAroundZ, -10, 10);


			//--------Force---------------
			RB.AddRelativeTorque(tiltAroundX * tiltX, yRotation, tiltAroundZ * tiltZ);

			//RB.AddRelativeTorque(MLx * tiltX, 0, MLz * tiltZ);


			//--------transform---------------
			//Quaternion target = Quaternion.Euler(0, yRotation, 0);
			//transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);



			//RB.AddRelativeTorque(MLx * tiltX, MLy , 0);


			RB.AddRelativeForce(Vector3.back * Mathf.Max(0f, ForwardForce * RB.mass));


			//RB.transform.localRotation = Quaternion.Euler(MLx * tiltX * Time.deltaTime, yRotation * Time.deltaTime, MLz * tiltZ * Time.deltaTime);


			//RB.transform.Rotate(0,0, MLz * tiltZ );





			//RB.AddRelativeForce(Vector3.left * maxForce * tiltAroundZ * 50);

			//RB.transform.Translate(tiltAroundZ * maxForce * Time.deltaTime, 0, 0);


			//RB.useGravity = false;

			//if (RB.velocity != MaxVelocity)
			//{

			//RB.AddForce(0, -Gravity, 0);

			//}


			

		}
	}

	

	

	

	

	

	public void Konumlama()

	{

		float sdeger_h = RB.velocity.magnitude * 8;
		float sdeger_y = Player.transform.position.y * 5;


		//RB.angularDrag = Mathf.Clamp(sdeger_h / 200, 0.1f, 2f);
		//ySpeed = Mathf.Clamp(sdeger_h / 10, -20, 0);

		float y1 = sdeger_y + 170;

		

		//seri.WriteLine(hiz.text);

		





		rotx = transform.localEulerAngles.x;
		rotx = (rotx > 180) ? rotx - 360 : rotx;


		rotz = transform.localEulerAngles.z;
		rotz = (rotz > 180) ? rotz - 360 : rotz;




		

	}


	public void Pitch()
	{

		//---------------Pitch-------------------

		if ((MLx < 0 && MLz < 0) || (MLx > 0 && MLz > 0))
		{
			RB.AddRelativeTorque(MLx * Mathf.Max(0f, tiltX), 0, 0);



			//Resulting_Value_from_Input += MLx * Rotation_Speed * Rotation_Friction; //You can also use "Mouse X"
			//Quaternion_Rotate_From = transform.rotation;
			//Quaternion_Rotate_To = Quaternion.Euler(Resulting_Value_from_Input, 0 , 0);

			//RB.transform.localRotation = Quaternion.Lerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);

			//RB.transform.localRotation = Quaternion.Euler(MLx * Mathf.Max(0f, tiltX), 0, 0);
		}




		//else RB.AddRelativeTorque(0,0,0);
	}


	public void Roll()
	{

		//---------------Roll---------------------
		if (MLx < 0 && MLz > 0)
		{




			RB.AddRelativeTorque(0, yRotation, MLz * tiltZ);
		}

		if (MLx > 0 && MLz < 0)
		{




			RB.AddRelativeTorque(0, yRotation, MLz * tiltZ);
		}
	}


	public void Rotate()
	{


		Quaternion_Rotate_From = transform.rotation;

		Quaternion_Rotate_To = Quaternion.Euler(0, 0, 0);

		transform.rotation = Quaternion.Slerp(Quaternion_Rotate_From, Quaternion_Rotate_To, Time.deltaTime * Rotation_Smoothness);
	}

	

	


}