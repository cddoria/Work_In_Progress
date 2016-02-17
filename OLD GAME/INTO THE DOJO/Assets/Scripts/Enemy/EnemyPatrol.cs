using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {
	
	public Transform target; //wherever the player is
	public Transform wayPoints; //Parent of waypoints tranforms
	public Transform sightStartPoint; //Start and end positions of enemy line of sight
	public Transform sightEndPoint1; //
	public Transform sightEndPoint2; //
	public Transform sightEndPoint3; //
	public Transform sightEndPoint4; //
	public Transform sightEndPoint5; //
	public Transform sightEndPoint6; //
	public Transform sightEndPoint7; //
	public Transform sightEndPoint8; //
	public Transform sightEndPoint9; //
	public Transform sightEndPoint10; //
	public Transform sightEndPoint11; //
	public Transform sightEndPoint12; //
	public Transform sightEndPoint13; //
	public Transform sightEndPoint14; //
	public Transform sightEndPoint15; //
	public Transform sightEndPoint16; //
	
	public bool seen1 = false; //whether the player is seen by an enemy
	public bool seen2 = false; //
	public bool seen3 = false; //
	public bool seen4 = false; //
	public bool seen5 = false; //
	public bool seen6 = false; //
	public bool seen7 = false; //
	public bool seen8 = false; //
	public bool seen9 = false; //
	public bool seen10 = false; //
	public bool seen11 = false; //
	public bool seen12 = false; //
	public bool seen13 = false; //
	public bool seen14 = false; //
	public bool seen15 = false; //
	public bool seen16 = false; //
	
	public float moveSpeed = 3f; //Speed of enemy movement
	
	Decompose decompScript; //Decompose script reference
	Animator enemyAnim; //Enemy animator
	
	private Transform nextWaypoint; //Children of parent waypoint
	private int next = 0; //Next waypoint target
	private int current = 0; //Current node in path1
	private float toPlayer; //Distance to player
	private float toTarget; //Distance to target
	private float radius = .01f; //Satisfaction radius
	private float attackRadius = 1.25f; //Attack satisfaction radius
	private bool seenOnce = false; //If player is seen once
	
	//Awake() instead of Start() for script referencing
	void Awake(){
		decompScript = GameObject.Find ("A* Controller").GetComponent<Decompose> ();
	}
	
	// Use this for initialization
	void Start () {
		wayPoints = GameObject.Find ("Waypoints").transform; //Finds and assigns the Waypoints' parent
															 //to the transform
		target = GameObject.Find ("Player").transform; //Finds and assigns the Waypoints' parent
													   //to the transform
		transform.position = wayPoints.GetChild(0).position; //Enemy's initial position will be first waypoint
		
		enemyAnim = GetComponent<Animator>(); //Initializing the enemy's animator
	}
	
	// Update is called once per frame
	void Update () {
		Linecasting ();
		if (!seenOnce) {
			Waypointpath ();
		}
		Move ();
	}
	
	//Create linecasts for player detection
	void Linecasting(){
		//Draws lines in every enemy direction
		Debug.DrawLine (sightStartPoint.position, sightEndPoint1.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint2.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint3.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint4.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint5.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint6.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint7.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint8.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint9.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint10.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint11.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint12.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint13.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint14.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint15.position, Color.green);
		Debug.DrawLine (sightStartPoint.position, sightEndPoint16.position, Color.green);
		
		//Will be true if LineCast touches the Player's collider
		seen1 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint1.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen2 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint2.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen3 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint3.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen4 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint4.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen5 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint5.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen6 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint6.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen7 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint7.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen8 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint8.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen9 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint9.position,
		                            1 << LayerMask.NameToLayer ("Player"));
		seen10 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint10.position,
		                             1 << LayerMask.NameToLayer ("Player"));
		seen11 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint11.position,
		                             1 << LayerMask.NameToLayer ("Player"));
		seen12 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint12.position,
		                             1 << LayerMask.NameToLayer ("Player"));
		seen13 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint13.position,
		                             1 << LayerMask.NameToLayer ("Player"));
		seen14 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint14.position,
		                             1 << LayerMask.NameToLayer ("Player"));
		seen15 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint15.position,
		                             1 << LayerMask.NameToLayer ("Player"));
		seen16 = Physics2D.Linecast (sightStartPoint.position, sightEndPoint16.position,
		                             1 << LayerMask.NameToLayer ("Player"));
	}
	
	//Enemy will move along waypoint path until it sees the player
	void Waypointpath(){
		if ((seen1 == false) && (seen2 == false) && (seen3 == false) && (seen4 == false) && (seen5 == false)
		    && (seen6 == false) && (seen7 == false) && (seen8 == false) && (seen9 == false) && (seen10 == false)
		    && (seen11 == false) && (seen12 == false) && (seen13 == false) && (seen14 == false) 
		    && (seen15 == false) && (seen16 == false)) {
			
			//Set next waypoint to the next child of the Waypoints transform
			nextWaypoint = wayPoints.GetChild (next);
			
			//Find the distance to the next waypoint
			Vector3 toWaypoint = nextWaypoint.position - transform.position;
			float distanceToWaypoint = toWaypoint.magnitude;
			
			//Move along waypoint path
			if (distanceToWaypoint < 0.1) {
				if ((next + 1) < wayPoints.childCount) {
					//Keeps moving to next waypoint
					next++;
				} else {
					//Reset enemy's position to first waypoint
					next = 0;
				}
			} else {
				if (nextWaypoint.position.x >= transform.position.x) {
					//Move toward next waypoint
					transform.position = Vector2.MoveTowards (transform.position, nextWaypoint.position,
					                                          moveSpeed * Time.deltaTime);
					//Face right
					transform.eulerAngles = new Vector2 (0, 0);
				}
				if (nextWaypoint.position.x <= transform.position.x) {
					//Move toward next waypoint
					transform.position = Vector2.MoveTowards (transform.position, nextWaypoint.position,
					                                          moveSpeed * Time.deltaTime);
				}
				if (nextWaypoint.position.x < transform.position.x) {
					//Face left
					transform.eulerAngles = new Vector2 (0, 180);
				}
			}
		} else {
			seenOnce = true;
			enemyAnim.SetBool("seen", true);
		}
	}
	
	//If Player has been seen once, break waypoint path and chase player
	void Move(){
		if (seenOnce == true) {
			//If no path1
			if (decompScript.path1 == null) {
				return;
			}
			
			//If reached the end of path1
			if (current >= decompScript.path1.Count) {
				return;
			}
			
			Vector2 playerDirection = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y); //get direction of player
			toPlayer = playerDirection.magnitude; //get horizontal distance
			Vector2 targetDirection = decompScript.path1[current].worldPos - new Vector2(transform.position.x, transform.position.y); //get direction of target
			toTarget = targetDirection.magnitude; //get horizontal distance	
			
			//If within next node's radius, move along path1
			if (toTarget > radius) {
				enemyAnim.SetTrigger("chase");
				
				if (decompScript.path1[current].worldPos.x >= transform.position.x) {
					//Move toward player
					transform.position = Vector3.MoveTowards (transform.position, decompScript.path1[current].worldPos,
					                                          moveSpeed * Time.deltaTime);
					//Face right
					transform.eulerAngles = new Vector3 (0, 0);
				}
				if (decompScript.path1[current].worldPos.x <= transform.position.x) {
					//Move toward next waypoint
					transform.position = Vector3.MoveTowards (transform.position, decompScript.path1[current].worldPos,
					                                          moveSpeed * Time.deltaTime);
				}
				if (decompScript.path1[current].worldPos.x < transform.position.x){
					//Face left
					transform.eulerAngles = new Vector3 (0, 180);
				}
			}
			
			//If player is within attack radius
			if(toPlayer < attackRadius){
				enemyAnim.SetTrigger("attack");
			} else{
				enemyAnim.SetTrigger("notAttacking");
				enemyAnim.SetTrigger("chase");
			}
		}
	}
}