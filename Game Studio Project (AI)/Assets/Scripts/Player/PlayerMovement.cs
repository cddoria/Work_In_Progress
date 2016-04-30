using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Transform player;
	public Rigidbody rb;
	public float rotationSpeed = 15f;
	public float moveSpeed = 10f;


	// Use this for initialization
	void Start () {
		player = transform.GetComponent<Transform> ();
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Movement (horizontal, vertical);
	}

	void Movement(float h, float v){
		if(h != 0f || v != 0f){
			Rotate (h, v);
			player.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		}
	}

	void Rotate(float h, float v){
		Vector3 targetDirection = new Vector3 (h, 0, v);

		Quaternion targetRotation = Quaternion.LookRotation (targetDirection, Vector3.up);

		Quaternion newRotation = Quaternion.Lerp (rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		rb.MoveRotation(newRotation);
	}
}
