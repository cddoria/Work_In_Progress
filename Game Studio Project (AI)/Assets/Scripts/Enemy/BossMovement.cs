/* Programmer: Kenneth Widemon
 * Description: A basic movement script for the boss. It'll move towards the enemy right away while following
 * 				its found path.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //To use Lists

public class BossMovement : MonoBehaviour {

	public List<Node> path; //List for path nodes
	public Transform trans; //Transform of this object
	public Transform target; //Transform of Player
	public float moveSpeed = 10f; //Max movement speed
	public float rotationSpeed = 360f; //Speed at which moving object rotates
	public float radius = .01f; //Satisfaction radius

	int current = 0; //Current node in path
	float toTarget; //Distance to target
	Rigidbody rb; //Rigidbody component of transform
	Vector3 targetDirection; //Direction of target
	Quaternion targetRotation; //Look at target
	Quaternion newRotation; //Updated rotation as transform moves
	FindPath pathfindingScript; //FindPath script reference

	//Use for Initialization
	void Start(){
		//This transform
		trans = GetComponent<Transform> ();
		//This rigidbody
		rb = GetComponent<Rigidbody> ();
		//Find and assign the target transform
		target = GameObject.Find ("Player").transform;

		pathfindingScript = GameObject.Find("AStar").GetComponent<FindPath> ();
	}

	// Update is called once per frame
	void Update () {
		//Find the path from the boss to the player every frame
		pathfindingScript.Find(trans.position, target.position);
		//Assign the boss's path as the path that was just found
		path = pathfindingScript.path;

		Move ();
	}

	//Moving the enemy toward the player if the enemy is no longer following waypoints
	void Move(){
		//If no path
		if (path == null) {
			return;
		}

		//If reached the end of path
		if (current >= path.Count) {
			return;
		}

		//Get direction of target
		targetDirection = path [current].worldPos - trans.position;
		//Avoid tilting
		targetDirection.y = 0;
		//Get horizontal distance
		toTarget = targetDirection.magnitude;	

		//If within next node's radius...
		if (toTarget > radius) {
			//Rotate towards target node
			Rotate ();
			//Move along path in forward direction
			trans.position = Vector3.MoveTowards (trans.position, path [current].worldPos, moveSpeed * Time.deltaTime);
		}
	}

	//Rotates the enemy to either the next waypoint or the player, depending on whether or not it's following the waypoints
	void Rotate(){
		targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		rb.MoveRotation (newRotation);
	}

	//FOR TESTING
	//Drawing the path
	void OnDrawGizmos(){
		//Draw path
		if (path != null) {
			foreach (Node n in path) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube (n.worldPos, Vector3.one);
			}
		}
	}
}