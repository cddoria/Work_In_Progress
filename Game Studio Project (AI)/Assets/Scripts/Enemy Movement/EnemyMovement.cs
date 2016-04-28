/* Programmer: Kenneth Widemon
 * Description: *** NEW KINEMATIC ARRIVE FOR A* ***
*/

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform trans; //Transform of this object
	public GameObject player; //Player transform
	public GameObject waypointsParent; //Parent of waypoints
	public int numberOfWaypoints; //How many waypoints to create
	public float moveSpeed = 10f; //Max movement speed
	public float rotationSpeed = 360f; //Speed at which moving object rotates
	public float radius = .01f; //Satisfaction radius
	public float maxRayDistance = 5f; //Max distance of raycast
	public bool followWaypoints; //For testing and toggling between movement along waypoints and AStar path

	GameObject waypointChildren; //Children of waypoints
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
	Decompose decompScript; //Decompose script
	FindPath pathFindingScript; //FindPath script

	//Awake() instead of Start() for script referencing, occurs first at runtime
	void Awake(){
		decompScript = GameObject.Find ("AStar").GetComponent<Decompose> ();
		pathFindingScript = GameObject.Find ("AStar").GetComponent<FindPath> ();

	}

	//Initialize
	void Start(){
		trans = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();

		if (waypointsParent == null && numberOfWaypoints != 0) {
			CreateWaypoints (numberOfWaypoints); //Create new waypoints
		}else if(waypointsParent == null || numberOfWaypoints == 0){
			waypointsParent = GameObject.Find ("Waypoints"); //Finds and assigns the Waypoints' parent
		}

		trans.position = waypointsParent.transform.GetChild(0).position; //Enemy's initial position will be first waypoint
		followWaypoints = true;

		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		RayCast ();
		FollowWaypoints ();
	}

	//Create a set of waypoints if not originally created
	void CreateWaypoints(int numWaypoints){
		
		waypointsParent = new GameObject ("Waypoints");
		waypointsParent.transform.position = Vector3.zero;

		for (int i = 0; i < numWaypoints; i++) {
			waypointChildren = new GameObject ("waypoint");

			waypointChildren.transform.position = new Vector3 (Random.Range ((-decompScript.worldSize.x / 2), (decompScript.worldSize.x / 2)), 0, Random.Range ((-decompScript.worldSize.y / 2), (decompScript.worldSize.y / 2)));

			if (!Physics.CheckSphere (waypointChildren.transform.position, decompScript.radius, decompScript.unwalkable)) {
				continue;
			} else {

			}

			waypointChildren.transform.parent = waypointsParent.transform;
		}
	}

	//A Raycast is shot from the Enemy's position along the forward vector and detects collision with the Player
	void RayCast(){
		RaycastHit hit;
		Vector3 forward = trans.TransformDirection (Vector3.forward) * maxRayDistance;

		Debug.DrawRay (trans.position, forward, Color.blue);

		if (Physics.Raycast(trans.position, forward, out hit)){
			if (hit.collider.tag == "Player") {
				followWaypoints = false;
			}
		}
	}

	//Enemy will move along waypoint path until it sees the player
	void FollowWaypoints(){
		if (followWaypoints == true) {
			pathFindingScript.target = nextWaypoint;
			//Set next waypoint to the next child of the Waypoints transform
			nextWaypoint = waypointsParent.transform.GetChild (next);

			//Find the distance to the next waypoint
			waypointDirection = nextWaypoint.position - transform.position;
			toWaypoint = waypointDirection.magnitude;

			//Move along waypoint path
			if (toWaypoint < 0.1) {
				next += nextDirection; //Increment the next waypoint counter

				//Reverse waypoint path
				if (next >= waypointsParent.transform.childCount) {
					next = waypointsParent.transform.childCount - 1; //Decrement the next waypoint counter
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
		} else if(followWaypoints == false){
			pathFindingScript.target = player.transform;
			Move ();
		}
	}

	void Move(){
		//If no path
		if (decompScript.path == null) {
			return;
		}

		//If reached the end of path
		if (current >= decompScript.path.Count) {
			return;
		}

		targetDirection = decompScript.path [current].worldPos - trans.position; //get direction of target
		targetDirection.y = 0; //Avoids tilting
		toTarget = targetDirection.magnitude; //get horizontal distance	

		//If within next node's radius, move along path
		if (toTarget > radius) {
			//Rotate towards target
			Rotate ();
			//move in forward direction
			trans.position = Vector3.MoveTowards (trans.position, decompScript.path [current].worldPos, moveSpeed * Time.deltaTime);
		}
	}

	void Rotate(){
		if (followWaypoints) {
			targetRotation = Quaternion.LookRotation (waypointDirection, Vector3.up);

			newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			rb.MoveRotation (newRotation);
		} else {
			targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

			newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			rb.MoveRotation (newRotation);
		}
	}
}