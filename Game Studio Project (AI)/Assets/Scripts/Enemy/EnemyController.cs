/* Programmer: Kenneth Widemon
 * Description: *** Controller for Enemies***
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For Lists

public class EnemyController : MonoBehaviour {
	public Transform trans; //Enemy Controller transform (this)
	public Transform target; //Player transform

	FindPath pathfindingScript;

	// Use this for initialization
	void Awake () {
		pathfindingScript = GameObject.Find("AStar").GetComponent<FindPath> ();

		if (trans == null) {
			trans = GetComponent<Transform> ();
		}

		if (target == null) {
			target.position = GameObject.Find ("Player").transform.position;
		}

		for (int i = 0; i < trans.childCount; i++) {
			trans.GetChild (i).gameObject.AddComponent<EnemyMovement> ();
			trans.GetChild (i).GetComponent<EnemyMovement> ().ray = GameObject.Find ("Enemies").transform.GetChild (i).FindChild ("RayCast").GetComponent<Transform>();
			trans.GetChild (i).GetComponent<EnemyMovement> ().waypointsParent = GameObject.Find ("Waypoints").transform.GetChild (i);
		}
	}

	void Update() {
		for (int i = 0; i < trans.childCount; i++) {
			//Find the path from the enemy to the player every frame
			pathfindingScript.Find(trans.GetChild(i).position, target.position);
			trans.GetChild (i).GetComponent<EnemyMovement> ().path = pathfindingScript.path;
		}
	}
}
