/* Programmer: Kenneth Widemon
 * Description: Controller for enemies; Assigns the target for each enemy as well as initially assigning the enemy script to
 * 				each enemy and assigning each one's raycast ray and set of waypoints. This class also finds and assigns the 
 * 				path to from each enemy to its target.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For Lists

public class EnemyController : MonoBehaviour {
	public Transform trans; //Enemy Controller transform (this)
	public Transform target; //Player transform

	FindPath pathfindingScript; //FindPath script reference

	// Use this for initialization
	void Awake () {
		pathfindingScript = GameObject.Find("AStar").GetComponent<FindPath> ();

		//If a transform has not yet been manually assigned in the editor...
		if (trans == null) {
			//Get this transform
			trans = GetComponent<Transform> ();
		}

		//If a target transform has not yet been manually assigned in the editor...
		if (target == null) {
			//Find the PLayer transform
			target = GameObject.Find ("Player").transform;
		}

		//For however many enemies there are...
		for (int i = 0; i < trans.childCount; i++) {
			//Assign the Enemy Movement script to each enemy
			trans.GetChild (i).gameObject.AddComponent<EnemyMovement> ();
			//Assign each enemy a raycast ray
			trans.GetChild (i).GetComponent<EnemyMovement> ().ray = GameObject.Find ("Enemies").transform.GetChild (i).FindChild ("RayCast").GetComponent<Transform>();
			//Assign each enemy a set of waypoints to follow
			trans.GetChild (i).GetComponent<EnemyMovement> ().waypointsParent = GameObject.Find ("Waypoints").transform.GetChild (i);
		}
	}

	void Update() {
		//For however many enemies there are...
		for (int i = 0; i < trans.childCount; i++) {
			//Find the path from the enemy to the player every frame
			pathfindingScript.Find(trans.GetChild(i).position, target.position);
			//Assign the enemy's path as the path that was just found
			trans.GetChild (i).GetComponent<EnemyMovement> ().path = pathfindingScript.path;
		}
	}
}
