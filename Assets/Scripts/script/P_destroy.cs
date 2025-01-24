using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_destroy : MonoBehaviour {

	public int health = 500;
	public int W_Damage = 1;

	public AudioSource missileExpSound;
	//public GameObject missileExpEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other){

		if (other.tag == "e_missile") {
			health -= W_Damage;
			//Instantiate(missileExpEffect,transform.position,Quaternion.identity);
			missileExpSound.Play ();
		
			Destroy (other.gameObject,2f);
		}
	}


//		void OnTriggerExit(Collider other){
//
//			if(other.tag == "e_missile"){
//				
//				//Instantiate(missileExpEffect,transform.position,Quaternion.identity);
//				//missileExpSound.Play();
//
//				Destroy(other.gameObject);
//			}
//		}





	}

