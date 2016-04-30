/* Programmer: Kenneth Widemon
 * Description: This script is attached to an empty gameobject called AStar and it decomposes the world
 * 				for the implentation of the A* algorithm in a 3D environment. It creates a grid with 
 * 				color-coded walkable and unwalkable areas.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //To use Lists

public class Decompose : MonoBehaviour {
	public LayerMask unwalkable; //Layermask for obstacles
	public Vector2 worldSize; //Size of the world of nodes
	public float radius = 1f; //Node radius
	public bool displayWorldGizmos; //Check true in editor to show world gizmos

	Node[,] world; //2D array of nodes in world
	Vector3 worldPoint; //World point for each collision check
	private float diameter; //Node diameter
	private int worldSizeX, worldSizeY; //X and Y coordinates of world
	bool walkable; //Whether obstacles are walkable

	//Initialization
	void Start() {
		diameter = radius * 2;
		worldSizeX = Mathf.RoundToInt (worldSize.x / diameter);
		worldSizeY = Mathf.RoundToInt (worldSize.y / diameter);

		DecomposeWorld(); //Initialize the world
	}

	//Returns the max size of the heap
	public int MaxSize{
		get{
			return worldSizeX * worldSizeY;
		}
	}

	//Create world
	void DecomposeWorld() {
		world = new Node[worldSizeX, worldSizeY];

		//Bottom-left point of world 
		Vector3 bottomLeft = transform.position - Vector3.right * (worldSize.x / 2) - Vector3.forward * (worldSize.y / 2);

		//Loop through all positions that will have nodes for collision checks
		for (int x = 0; x < worldSizeX; x++) {
			for (int y = 0; y < worldSizeY; y++) {
				//As x and y increase, keep incrementing until edge of world is reached
				worldPoint = bottomLeft + Vector3.right * (x * diameter + radius) + Vector3.forward * (y * diameter + radius);

				walkable = false;
				//Check for collisions with unwalkable objects on the grid
				if(!Physics.CheckSphere(worldPoint, radius, unwalkable)){
					walkable = true;
				}

				//Adds walkable areas to world
				world[x, y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	//Get the neighbor nodes of the player
	public List<Node> Neighbors(Node node) {
		List<Node> neighbors = new List<Node>();
		
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				//Checks diagonals and skips them when creating path (Manhattan Distance)
				if ((x == 0 && y == 0) || (x == -1 && y == -1) || (x == 1 && y == -1) || (x == -1 && y == 1) || (x == 1 && y == 1)){
					continue;
				}

				int checkX = node.worldX + x;
				int checkY = node.worldY + y;
				
				//Adds available/walkable neighbors to the neighbors list
				if (checkX >= 0 && checkX < worldSizeX && checkY >= 0 && checkY < worldSizeY) {
					neighbors.Add(world[checkX, checkY]);
				}
			}
		}
		
		return neighbors;
	}
	
	//Access the node from the world point for path
	public Node FromWorldPoint(Vector3 worldPos) {
		float percentX = (worldPos.x + worldSize.x / 2) / worldSize.x;
		float percentY = (worldPos.z + worldSize.y / 2) / worldSize.y;

		//Clamp percentages between 0 and 1 to avoid bounds errors
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		
		int x = Mathf.RoundToInt((worldSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((worldSizeY - 1) * percentY);

		return world[x, y];
	}

	//FOR TESTING
	//Drawing the world grid
	void OnDrawGizmos(){
		//Base of grid
		Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));

		//If checked in editor...
		if (world != null && displayWorldGizmos) {
			//Draw obstacles and walkable areas
			foreach (Node n in world) {
				if(n.walkable){
					//WALKABLE = WHITE
					Gizmos.color = Color.white;
				}else{
					//UNWALKABLE = RED
					Gizmos.color = Color.red;
				}

				//Draw Node as a cube
				Gizmos.DrawCube(n.worldPos, Vector3.one * (diameter - .1f));
			}
		}
	}
}