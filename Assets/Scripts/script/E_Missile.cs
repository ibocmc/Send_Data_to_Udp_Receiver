using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Missile : MonoBehaviour {

	public GameObject hitEffect;
	public LayerMask truck;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit; 

		if(Physics.Raycast(transform.position,transform.forward,out hit,truck)){


			//Instantiate (hitEffect2, hit.point, Quaternion.LookRotation (hit.normal));
			if(hit.collider.tag == "player"){
				Instantiate (hitEffect, hit.point, Quaternion.identity /*Quaternion.LookRotation (hit.normal)*/);
				//GameObject.Destroy (hitEffect);
			}
		}
		
	}
}
