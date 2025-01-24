using UnityEngine;
using System.Collections;

public class missileMover : MonoBehaviour {


	//public AudioSource roc_sound;
	//public AudioSource roc_Exp_Sound;

	//public Transform roc_exp_T;
	//public GameObject roc_Exp_E;



	public float speed;
	public float r_speed;
	public Rigidbody rb;

	//public AudioSource missile_sound;




	// Use this for initialization
	void Start () {

	
	
	}
	
	// Update is called once per frame
	void FixedUpdate() {

//		RaycastHit hit;
//
//		if (Physics.Raycast (roc_exp_T.transform.position, roc_exp_T.transform.forward, out hit,4f)) {
//
//			if (hit.collider.tag == "ground") {
//				//roc_Exp_Sound.Play ();	
//				Instantiate (roc_Exp_E, hit.point, Quaternion.LookRotation (hit.normal));
//			} 
//
//			if (hit.collider.tag == "surface") {
//				//roc_Exp_Sound.Play ();	
//				Instantiate (roc_Exp_E, hit.point, Quaternion.LookRotation (hit.normal));
//			} 
//
//		}


		//missile_sound.Play ();

		//roc_sound.GetComponent<AudioSource> ().Play ();
		rb.AddRelativeForce( Vector3.forward * speed,ForceMode.Force );
		//rb.velocity = transform.forward * speed ;
		//rb.AddTorque(transform.forward * r_speed,ForceMode.VelocityChange);
	}


//	void OnTriggerEnter(Collider other){
//		if(other.tag == "ground" ){
//
//
//
//			//roc_Exp_Sound.Play ();
//			Instantiate (roc_Exp_E, roc_exp_T.position, roc_exp_T.rotation);
//			Destroy (gameObject);
//		}
//
//		if(other.tag == "surface" ){
//
//			//roc_Exp_Sound.Play ();
//
//			Instantiate (roc_Exp_E, roc_exp_T.position, roc_exp_T.rotation);
//			Destroy (gameObject);
//		}
//
//	}



}
