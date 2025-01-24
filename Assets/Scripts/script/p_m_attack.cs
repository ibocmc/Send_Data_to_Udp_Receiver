using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class p_m_attack : MonoBehaviour{

	//public float nextFire;
	//public float fireRate;
	//public Transform m_spawn;
	public List<GameObject> targets;
	public GameObject lockedTarget;
	public GameObject player;
	public bool hasTarget;
	public float discoveryAngle;
	public List<GameObject> drawableTarget;

	public AudioSource lockSound;

	public GameObject missile;

	public Camera playerCamera;

	public Texture2D lockedTargetIndicator;
	public Texture2D targetIndicator;

	public float m_speed;
	public float turn;

	public static bool openMissile = false;
	public static bool openMissile2 = false;

	//public static bool lock_alert;

	//Use this for initialization
void Start ()
{



	// first init of targets
	GameObject[] targets = GameObject.FindGameObjectsWithTag ("enemy");
	player = GameObject.Find ("rotater");
	
	// detect enemies 
	InvokeRepeating ("FindTargetInRange", 1, 0.5F);
}



void FindTargetInRange()
{
	
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("enemy");
	
		lockedTarget = null;
	
		drawableTarget.Clear ();
	
		hasTarget = false;
	
		openMissile2 = false;
		openMissile = false;
		//lock_alert = false;
	
		float smallerAngle = discoveryAngle;
	
	foreach (GameObject enemyTarget in targets) {
		// object is on front of player
		Vector3 onViewPoint = playerCamera.WorldToViewportPoint (enemyTarget.transform.position);
		if (onViewPoint.z > 0 && onViewPoint.x > 0 && onViewPoint.x < 1 && onViewPoint.y > 0 && onViewPoint.y < 1) {
			
			
			Vector3 here = player.transform.position; // get player position...
			Vector3 dir = enemyTarget.transform.position - here; // find enemy direction
			int enemyAngle = Mathf.RoundToInt (Vector3.Angle (dir, player.transform.forward)); 
			if (enemyAngle <= discoveryAngle / 2) { // if inside the angle...
				if (enemyAngle < smallerAngle) {
					if (lockedTarget != null)
						
						drawableTarget.Add (lockedTarget);

						lockedTarget = enemyTarget;

						hasTarget = true;
						//lock_alert = true;
						if(hasTarget){

							openMissile = true;
							openMissile2 = true;

							//lockSound.Play();
					
						}
						else
							//lockSound.Stop();

						openMissile = false;
						openMissile2 = false;

						smallerAngle = enemyAngle;
						
						continue;
						
				}
				
					drawableTarget.Add (enemyTarget);

			}
		}
	} 
}
				
void OnGUI ()
{
	//findTargetInRange ();
	if (!lockedTargetIndicator) {
		Debug.LogError ("Assign a Texture in the inspector.");
		return;
	}
	
	if (lockedTarget != null) {
		
			Vector3 screenPosition = playerCamera.WorldToScreenPoint (lockedTarget.transform.position);
		
			GUI.DrawTexture (new Rect (screenPosition.x - 25, (Screen.height - screenPosition.y - 25), 50, 50), lockedTargetIndicator, ScaleMode.ScaleToFit, true, 1);



	}
	
	foreach (GameObject enemyTarget in drawableTarget) {
		if (enemyTarget != null) {
			Vector3 screenPosition = playerCamera.WorldToScreenPoint (enemyTarget.transform.position);
			GUI.DrawTexture (new Rect (screenPosition.x - 25, (Screen.height - screenPosition.y - 25), 50, 50), targetIndicator, ScaleMode.ScaleToFit, true, 1);

		}
	}
	
}

	void FixedUpdate(){




		GameObject go = GameObject.FindGameObjectWithTag("missile");

		missile = go;



		if(hasTarget){

		missile.transform.rotation = Quaternion.Slerp(missile.transform.rotation, Quaternion.
		                         LookRotation(lockedTarget.transform.position-missile.transform.position),turn * Time.deltaTime);
		
		
				missile.transform.position += missile.transform.forward * m_speed * Time.deltaTime;

		}



	}
	}



