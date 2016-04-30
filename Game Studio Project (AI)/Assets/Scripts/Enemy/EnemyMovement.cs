/* Programmer: Kenneth Widemon
 * Description: *** NEW KINEMATIC ARRIVE FOR A* ***
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //To use Lists

public class EnemyMovement : MonoBehaviour {

	public List<Node> path; //List for path nodes
	public Transform trans; //Transform of this object
	public Transform ray; //Transform of raycast object
	public Transform waypointsParent; //Parent of waypoints
	public float moveSpeed = 10f; //Max movement speed
	public float rotationSpeed = 360f; //Speed at which moving object rotates
	public float radius = .01f; //Satisfaction radius
	public float maxRayDistance = 10f; //Max distance of raycast
	public bool followWaypoints; //For testing and toggling between movement along waypoints and AStar path

	Transform nextWaypoint; //Children of parent waypoint
	int next = 0; //Next waypoint target
	int nextDirection = 1; //Adjustor for reversing waypoint path
	int current = 0; //Current node in path
	float toTarget; //Distance to target
	float toWaypoint; //Distance to waypoint
	Rigidbody rb; //Rigidbody component of transform
	Vector3 targetDirection; //Direction of target
	Vector3 waypointDirection; //Direction of waypoint
	Quaternion targetRotation; //Look at target
	Quaternion newRotation; //Updated rotation as transform moves

	//Initialize
	void Start(){
		trans = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
		trans.position = waypointsParent.GetChild(0).position; //Enemy's initial position will be first waypoint
		followWaypoints = true; //Whether to follow the waypoints
	}

	// Update is called once per frame
	void Update () {
		RayCast ();
		FollowWaypoints ();
		Move ();
	}

	//A Raycast is shot from the Enemy's position along the forward vector and detects collision with the Player
	void RayCast(){
		RaycastHit hit;
		Ray raycast = new Ray (ray.position, trans.forward);

		Debug.DrawRay (ray.position, trans.forward * maxRayDistance, Color.blue);

		if (Physics.Raycast(raycast, out hit, maxRayDistance)){
			if (hit.collider.tag == "Player") {
				//Debug.Log ("Hit");
				followWaypoints = false;
			}
		}
	}

	//Enemy will move along waypoint path until it sees the player
	void FollowWaypoints(){
		if (followWaypoints == true) {
			//Set next waypoint to the next child of the Waypoints transform
			nextWaypoint = waypointsParent.GetChild (next);

			//Find the distance to the next waypoint
			waypointDirection = nextWaypoint.position - transform.position;
			toWaypoint = waypointDirection.magnitude;

			//Move along waypoint path
			if (toWaypoint < 0.1) {
				next += nextDirection; //Increment the next waypoint counter

				//Reverse waypoint path
				if (next >= waypointsParent.childCount) {
					next = waypointsParent.childCount - 1; //Decrement the next waypoint counter
					nextDirection = -1;
				} else if (next < 0) {
					//Reset enemy's position to first waypoint
					next = 0;
					nextDirection = 1;
				}
			} else {
				//Rotate towards target
				Rotate ();
				//move in forward direction
				trans.position = Vector3.MoveTowards (trans.position, nextWaypoint.position, moveSpeed * Time.deltaTime);
			}
		}
	}

	void Move(){
		if (followWaypoints == false) {
			//If no path
			if (path == null) {
				return;
			}

			//If reached the end of path
			if (current >= path.Count) {
				return;
			}

			targetDirection = path [current].worldPos - trans.position; //get direction of target
			targetDirection.y = 0; //Avoids tilting
			toTarget = targetDirection.magnitude; //get horizontal distance	

			//If within next node's radius, move along path
			if (toTarget > radius) {
				//Rotate towards target
				Rotate ();
				//move in forward direction
				trans.position = Vector3.MoveTowards (trans.position, path [current].worldPos, moveSpeed * Time.deltaTime);
			}
		}
	}

	void Rotate(){
		if (followWaypoints == true) {
			targetRotation = Quaternion.LookRotation (waypointDirection, Vector3.up);

			newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			rb.MoveRotation (newRotation);
		} else {
			targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

			newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			rb.MoveRotation (newRotation);
		}
	}

	//FOR TESTING
	//Drawing the path
	void OnDrawGizmos(){
		//Only draw path
		if (path != null) {
			foreach (Node n in path) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube (n.worldPos, Vector3.one);
			}
		}
	}
}