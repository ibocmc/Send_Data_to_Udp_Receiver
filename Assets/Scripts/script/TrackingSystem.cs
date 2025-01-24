using UnityEngine;
using System.Collections;

public class TrackingSystem : MonoBehaviour {
	public float speed = 3.0f;

	public GameObject m_target = null;
	Vector3 m_lastKnownPosition = Vector3.zero;
	Quaternion m_lookAtRotation;


	void Start(){

//		GameObject go = GameObject.FindGameObjectWithTag("truck");
//		go = m_target;


		//GameObject[] targets = GameObject.FindGameObjectsWithTag ("enemy");
	}

	// Update is called once per frame
	void Update () {

		if (m_target == null) {


			m_target = GameObject.FindGameObjectWithTag ("truck");

		}




			if (m_target) {
			
			if (m_lastKnownPosition != m_target.transform.position) {
				m_lastKnownPosition = m_target.transform.position;
					m_lookAtRotation = Quaternion.LookRotation (m_lastKnownPosition - transform.position);
				}

				if (transform.rotation != m_lookAtRotation) {
					transform.rotation = Quaternion.RotateTowards (transform.rotation, m_lookAtRotation, speed * Time.deltaTime);
				}
			}

	}
	public void SetTarget(GameObject target){

//			GameObject go = GameObject.FindGameObjectWithTag("truck");
//
//			go = m_target;



			m_target = target;


	}
}
