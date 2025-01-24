using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shootingSystem : MonoBehaviour {
	public float fireRate;
	public int damage;
	public float fieldOfView;
	public bool beam;
	public GameObject projectile;
	public List<GameObject> projectileSpawns;

	List<GameObject> m_lastProjectiles = new List<GameObject>();
	float m_fireTimer = 0.0f;
	public GameObject m_target;

	void Start(){

//		GameObject m_target = GameObject.FindGameObjectWithTag("truck");
		//GameObject[] targets = GameObject.FindGameObjectsWithTag ("enemy");
	}

	// Update is called once per frame
	void Update () {

		if (m_target == null) {


			m_target = GameObject.FindGameObjectWithTag ("truck");

		}



		if(!m_target)
		{
			if(beam)
				RemoveLastProjectiles();

			return;
		}

		if(beam && m_lastProjectiles.Count <= 0){
			float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));

			if(angle < fieldOfView){
				SpawnProjectiles();
			}
		}else if(beam && m_lastProjectiles.Count > 0){
			float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));

			if(angle > fieldOfView){
				RemoveLastProjectiles();
			}
		}else{
			m_fireTimer += Time.deltaTime;

			if(m_fireTimer >= fireRate){
				float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(m_target.transform.position - transform.position));

				if(angle < fieldOfView){
					SpawnProjectiles();

					m_fireTimer = 0.0f;
				}
			}
		}
	}

	void SpawnProjectiles(){
		if(!projectile){
			return;
		}

		m_lastProjectiles.Clear();

		for(int i = 0; i < projectileSpawns.Count; i++){
			if(projectileSpawns[i]){
				GameObject proj = Instantiate(projectile, projectileSpawns[i].transform.position, projectileSpawns[i].transform.rotation) as GameObject;
				proj.GetComponent<BaseProjectile>().FireProjectile(projectileSpawns[i], m_target, damage, fireRate);

				m_lastProjectiles.Add(proj);
			}
		}
	}

	public void SetTarget(GameObject target){
		
		

			m_target = target;



	}

	void RemoveLastProjectiles()
	{
		while(m_lastProjectiles.Count > 0){
			Destroy(m_lastProjectiles[0]);
			m_lastProjectiles.RemoveAt(0);
		}
	}
}