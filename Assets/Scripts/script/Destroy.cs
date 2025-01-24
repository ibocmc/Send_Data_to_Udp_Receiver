using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Destroy (gameObject, 3f);
		
	}

//	void OnTriggerEnter(Collider other){
//
//		if(other.tag == "missile"){
//			
//			Destroy (gameObject);
//
//
//
//		}


	}

