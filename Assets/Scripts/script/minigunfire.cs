using UnityEngine;
using System.Collections;

public class minigunfire : MonoBehaviour {

//	public AudioSource g_Hit;
//	public AudioSource s_Hit;
	public LayerMask ground;
	public LayerMask metalic_surface;

	public GameObject mgFire;
	//public Transform mg;

	public GameObject bullet_1;

	public GameObject hitEffect;
	public GameObject hitEffect2;
	public GameObject hitEffect3;


	public Transform bullet_1_spawn;
	public Transform bullet_2_spawn;
	public Transform bullet_3_spawn;

	public Transform fireSpawn;
	public AudioSource mgsound;
	//public AudioSource mgsound_last;
	//public Rigidbody rb;

	public float speed;


	public float fireRate;
	public float nextFire;

	//public Animator anim;


	// Use this for initialization
	void Start () {
		//mgsound = GetComponent<AudioSource> ();
		//mgsound_last = GetComponent<AudioSource> ();
		//rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		if (Input.GetMouseButton(0) && Time.time > nextFire) {

			//mgsound.Play ();

			nextFire = Time.time + fireRate;
			Instantiate (mgFire, fireSpawn.position, fireSpawn.rotation);
			Instantiate (bullet_1, bullet_1_spawn.position, bullet_1_spawn.rotation);
			Instantiate (bullet_1, bullet_2_spawn.position, bullet_2_spawn.rotation);
			Instantiate (bullet_1, bullet_3_spawn.position, bullet_3_spawn.rotation);



			if(Physics.Raycast(bullet_1_spawn.transform.position,bullet_1_spawn.transform.forward,out hit)){

			//	Instantiate (hitEffect, hit.point, Quaternion.LookRotation (hit.normal));

			    Instantiate (hitEffect, hit.point, Quaternion.identity /*Quaternion.LookRotation (hit.normal)*/);
				Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
				if (hit.collider.tag == "ground") {
					//g_Hit.Play ();	
					Instantiate (hitEffect3, hit.point, Quaternion.LookRotation (hit.normal));
				} 
			
			}

			if(Physics.Raycast(bullet_2_spawn.transform.position,bullet_2_spawn.transform.forward,out hit)){

				Instantiate (hitEffect, hit.point, Quaternion.identity /*Quaternion.LookRotation (hit.normal)*/);
			    Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
				if(hit.collider.tag == "ground"){
					Instantiate (hitEffect3, hit.point, Quaternion.LookRotation (hit.normal));
				}
			}

			if(Physics.Raycast(bullet_3_spawn.transform.position,bullet_3_spawn.transform.forward,out hit)){

				Instantiate (hitEffect, hit.point,Quaternion.identity  /* Quaternion.LookRotation (hit.normal)*/);
				Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
//				Instantiate (hitEffect3, hit.point, Quaternion.LookRotation (hit.normal));
			}


//
//			if(Physics.Raycast(bullet_1_spawn.transform.position,bullet_1_spawn.transform.forward,out hit,metalic_surface.value)){
//
//				//	Instantiate (hitEffect, hit.point, Quaternion.LookRotation (hit.normal));
//
//				Instantiate (hitEffect, hit.point, Quaternion.identity /*Quaternion.LookRotation (hit.normal)*/);
//				Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
//				//Instantiate (hitEffect3, hit.point, Quaternion.LookRotation (hit.normal));
//
//			}
//
//			if(Physics.Raycast(bullet_2_spawn.transform.position,bullet_2_spawn.transform.forward,out hit,metalic_surface.value)){
//
//				Instantiate (hitEffect, hit.point, Quaternion.identity /*Quaternion.LookRotation (hit.normal)*/);
//				Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
//				//Instantiate (hitEffect3, hit.point, Quaternion.LookRotation (hit.normal));
//			}
//
//			if(Physics.Raycast(bullet_3_spawn.transform.position,bullet_3_spawn.transform.forward,out hit,metalic_surface.value)){
//
//				Instantiate (hitEffect, hit.point,Quaternion.identity  /* Quaternion.LookRotation (hit.normal)*/);
//				Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
//		      //Instantiate (hitEffect3, hit.point, Quaternion.LookRotation (hit.normal));
//			}
//





		}


		if(Input.GetKey(KeyCode.Mouse0)){


			transform.Rotate(0f,0f,Time.deltaTime * speed);
			//rb.AddTorque(transform.forward*speed,ForceMode.VelocityChange);

		}









		if(Input.GetMouseButtonDown(0)){
			mgsound.mute = false;
			//mgsound_last.mute = true;
			mgsound.Play ();

		}

		if (Input.GetMouseButtonUp (0)) {
			mgsound.mute = true;
			//mgsound_last.mute = false;
			//mgsound_last.Play ();
		}





	}


	void FixedUpdate(){





	}



//	void FixedUpdate(){
//
//		float h = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
//		float v = Input.GetAxis ("Vertical") * speed ;
//		rb.AddTorque (transform.up * v, ForceMode.Force);
//
//		if(Input.GetKey(KeyCode.G)){
//
//
//
//
//		}



	}

