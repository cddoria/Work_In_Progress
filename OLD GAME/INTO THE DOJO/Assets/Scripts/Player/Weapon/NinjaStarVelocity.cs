using UnityEngine;
using System.Collections;

public class NinjaStarVelocity : MonoBehaviour {

	// Use this for initialization
	public float speed;
	Rigidbody2D rb;
	public Vector3 targetDirection;

	void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = targetDirection * speed;
	}

	public void setTargetDirection (Vector3 target)
	{
        targetDirection = (target - Camera.main.WorldToScreenPoint(transform.position)).normalized;
	}

    void OnTriggerEnter2D(Collider2D obj)
    {
		Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), obj);
    }
}