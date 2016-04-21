/* Programmer: Kenneth Widemon
 * Description: *** NEW KINEMATIC ARRIVE FOR A* ***
*/

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform trans; //choose which object to move
	public float moveSpeed = 10f; //max movement speed
	public float rotationSpeed = 360f; //speed at which moving object rotates
	public float radius = .01f; //satisfaction radius

	float toTarget; //distance to target
	int current = 0; //current node in path
	Rigidbody rb;
	Vector3 targetDirection;
	Quaternion targetRotation; //rotation towards target
	Quaternion newRotation;
	Decompose decompScript; //Decompose script

	//Awake() instead of Start() for script referencing
	void Awake(){
		decompScript = GameObject.Find ("AStar").GetComponent<Decompose> ();
		trans = transform.GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		Move ();
	}

	public void Move(){
		//If no path
		if (decompScript.path == null) {
			return;
		}

		//If reached the end of path
		if (current >= decompScript.path.Count) {
			return;
		}

		targetDirection = decompScript.path[current].worldPos - trans.position; //get direction of target
		targetDirection.y = 0; //Avoids tilting
		toTarget = targetDirection.magnitude; //get horizontal distance	

		//If within next node's radius, move along path
		if (toTarget > radius) {
			//Rotate towards target
			Rotate ();
			//move in forward direction
			trans.position = Vector3.MoveTowards(trans.position, decompScript.path[current].worldPos, moveSpeed * Time.deltaTime);
		}
	}

	void Rotate(){
		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		Quaternion newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		rb.MoveRotation(newRotation);
	}
}