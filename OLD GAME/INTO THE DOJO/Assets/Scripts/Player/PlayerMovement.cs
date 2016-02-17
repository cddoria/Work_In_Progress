using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 3f; //movement speed

	Animator playerAnim;

	// Use this for initialization
	void Start () {
		playerAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		Attack ();
	}

	void Movement(){
		//Set speed condition based on input from the horizontal axis using "A or D", or "Left or Right"
		playerAnim.SetFloat ("Xspeed", Mathf.Abs (Input.GetAxis("Horizontal")));
		playerAnim.SetFloat ("Yspeed", Mathf.Abs (Input.GetAxis("Vertical")));

		//GetAxisRaw makes using animations easier
		//Move right
		if (Input.GetAxisRaw("Horizontal") > 0.1) {
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			//Face right
			transform.eulerAngles = new Vector2 (0, 0);
		}

		//Move left
		if(Input.GetAxisRaw("Horizontal") < 0){
			transform.Translate(Vector2.right * speed * Time.deltaTime);
			//Face left
			transform.eulerAngles = new Vector2(0, 180);
		}

		//Move up
		if (Input.GetAxisRaw ("Vertical") > 0.1) {
			playerAnim.SetBool ("walkingUp", true);
			transform.Translate (Vector2.up * speed * Time.deltaTime);
		} else {
			playerAnim.SetBool("walkingUp", false);
		}
		
		//Move left
		if (Input.GetAxisRaw ("Vertical") < 0) {
			playerAnim.SetBool ("walkingDown", true);
			transform.Translate (Vector2.down * speed * Time.deltaTime);
		} else {
			playerAnim.SetBool("walkingDown", false);
		}
	}

	void Attack(){
		if (Input.GetMouseButtonDown(0)) {
			playerAnim.SetTrigger ("attack");
		} else {
			playerAnim.SetTrigger("stopAttacking");
		}
	}
}
