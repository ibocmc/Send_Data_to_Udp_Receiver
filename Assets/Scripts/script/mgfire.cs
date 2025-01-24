using UnityEngine;
using System.Collections;

public class mgfire : MonoBehaviour {


	public GameObject mgFire;
	public GameObject bullet_mg;

	public Transform bullet_spawn;
	public Transform fireSpawn;
	public AudioSource mgsound;
	//public AudioSource mgsound_s;

	public float fireRate;
	public float nextFire;

	public Animator anim;


	// Use this for initialization
	void Start () {
		//mgsound.GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		anim.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton(0) && Time.time > nextFire) {
			
			//mgsound_s.Play ();
			nextFire = Time.time + fireRate;
			Instantiate (mgFire, fireSpawn.position, fireSpawn.rotation);
			Instantiate (bullet_mg, bullet_spawn.position, bullet_spawn.rotation);
		} 
			
		if (Input.GetMouseButton(0)) {

			anim.enabled = true;
			//mgsound.Play ();


			//anim.Play ("Basic_Fire", 1, 0);


		} else {
			anim.enabled = false;
			//mgsound.Stop ();
		}


		if(Input.GetMouseButtonDown(0)){
			mgsound.mute = false;
			//mgsound_s.mute = true;
			mgsound.Play ();
			//mgsound_s.Play();
		}

		if (Input.GetMouseButtonUp (0)) {
			mgsound.mute = true;

		}



	}
}
