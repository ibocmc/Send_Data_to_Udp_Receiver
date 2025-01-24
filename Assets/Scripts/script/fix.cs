using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fix : MonoBehaviour {

	//public Transform target;
	public int a;
	public int b;
	public int c;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.localEulerAngles = new Vector3 (0, 0, 0);
		//transform.position = target.position;

	}
}
