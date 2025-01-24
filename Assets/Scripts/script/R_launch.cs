using UnityEngine;
using System.Collections;

public class R_launch : MonoBehaviour {


	public GameObject h_rocket;
	public Transform fireSpawn;
	//public AudioSource roc_sound;
	//public Rigidbody rb;

	//public float roc_speed;


	public float fireRate;
	public float nextFire;




	// Use this for initialization
	void Start () {
		//roc_sound.GetComponent<AudioSource> ();


	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Mouse1) && Time.time > nextFire) {

			//roc_sound.Play ();
			nextFire = Time.time + fireRate;
			Instantiate (h_rocket, fireSpawn.position, fireSpawn.rotation);
			//rb.velocity = transform.forward * roc_speed;
		} 





	}
}
