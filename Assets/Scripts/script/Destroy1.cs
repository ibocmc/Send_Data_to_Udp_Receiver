﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if(other.tag == "enemy"){
			
			Destroy (gameObject);



		}


	}
}
