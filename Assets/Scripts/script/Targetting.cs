using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour 
{
	GameObject[] enemies;
	public Transform selectedTarget;
	public Transform nearestEnemy;
	public Vector3 enemyCenter;

	public float minimumTargetDistance = 100.0f;

	private float lastFrameLockOn;

	GameObject[] lockOnTransforms;

	//Transform of the UI element
	public Transform lockOn;

	// Use this for initialization
	void Start () 
	{    
		lastFrameLockOn = Input.GetAxis("LockOn");
		selectedTarget = null;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetAxis("LockOn") > 0.5f )
		{
			if(Input.GetAxis("LockOn") > 0.5f && lastFrameLockOn < 0.5f)
			{
				targetCloseEnemies();
			}

			if(selectedTarget)
			{
				transform.LookAt(selectedTarget.position);
			}

			if(nearestEnemy)
			{
				if(selectedTarget != nearestEnemy)
				{
					TargetEnemy(nearestEnemy);
				}
			}

		}
		if(Input.GetAxis("LockOn") <= 0.5f)
		{
			selectedTarget = null;
			nearestEnemy = null;
			enemyCenter = Vector3.zero;
			DestroyLockOns();
		}

		lastFrameLockOn = Input.GetAxis("LockOn");
	}

	private void targetCloseEnemies()
	{
		// Find all enemies within the scene and add them to a list
		enemies = GameObject.FindGameObjectsWithTag("Enemy");

		//Add enemies that are within range to a new list of targetable enemies
		foreach(GameObject enemy in enemies) // list of all enemies
		{
			float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position); // distance of new enemy to player
			if(enemyDistance <= minimumTargetDistance) // checks if the new enemy is close enough to target
			{

				if(nearestEnemy)
				{
					if(enemyDistance < Vector3.Distance(transform.position, nearestEnemy.position )) // checks if new enemy is closer than existing enemy
					{
						nearestEnemy = enemy.transform;
					}
				}
				else
				{

					nearestEnemy = enemy.transform; // if the nearest enemy doesn't exist, the new enemy is assigned to nearestenemy
				}
			}
		}

		if(nearestEnemy)
		{
			Bounds bounds = new Bounds (nearestEnemy.transform.position, Vector3.one);

			Renderer[] renderers = nearestEnemy.GetComponentsInChildren<MeshRenderer>();

			foreach (Renderer renderer in renderers)

			{
				bounds.Encapsulate (renderer.bounds);
			}

			renderers = nearestEnemy.GetComponentsInChildren<SkinnedMeshRenderer>();

			foreach (Renderer renderer in renderers)
			{
				bounds.Encapsulate (renderer.bounds);
			}

			Debug.Log ("bounds center: "+bounds.center);

			enemyCenter = bounds.center;

		}
	}

	public void TargetEnemy(Transform target)
	{    

		selectedTarget = target;

		DestroyLockOns();

		if (selectedTarget)
		{
			Transform lockOnClone = Instantiate(lockOn, Vector3.zero, Quaternion.identity) as Transform;
			lockOnClone.parent = selectedTarget.transform;
			lockOnClone.position = enemyCenter;
		}
	}

	void DestroyLockOns()
	{
		lockOnTransforms = GameObject.FindGameObjectsWithTag("LockOn");
		//Debug.Log ("found " + lockOnTransforms.Length + " lockons");
		foreach(GameObject lockOnTransform in lockOnTransforms)
		{
			Debug.Log ("Destroying lockon");
			Destroy(lockOnTransform);
		}
	}
}