/* Programmer: Kenneth Widemon
 * Description: This script is attached to an empty gameobject called AStar and it decomposes the world
 * 				for the implentation of the A* algorithm in a 3D environment. It creates a grid with 
 * 				color-coded walkable and unwalkable areas.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //To use Lists

public class Decompose : MonoBehaviour {

	public List<Node> path; //List for path nodes
	public LayerMask unwalkable; //Layermask for obstacles
	public Vector2 worldSize; //Size of the world of nodes
	public float radius; //Node radius
	public bool onlyDisplayPathGizmos; //Check true in editor to only show path gizmos

	Node[,] world; //2D array of nodes in world
	Vector3 worldPoint;
	private float diameter; //Node diameter
	private int worldSizeX, worldSizeY; //X and Y coordinates of world
	bool walkable;
	
	void Start() {
		diameter = radius * 2;
		worldSizeX = Mathf.RoundToInt (worldSize.x / diameter);
		worldSizeY = Mathf.RoundToInt (worldSize.y / diameter);

		DecomposeWorld(); //Initialize the world
	}

	public int MaxSize{
		get{
			return worldSizeX * worldSizeY;
		}
	}

	//Create world
	void DecomposeWorld() {
		world = new Node[worldSizeX, worldSizeY];

		Vector3 bottomLeft = transform.position - Vector3.right * (worldSize.x / 2) - Vector3.forward * (worldSize.y / 2);
		
		for (int x = 0; x < worldSizeX; x++) {
			for (int y = 0; y < worldSizeY; y++) {
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
				//Checks diagonals and skips them when creating path
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
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		
		int x = Mathf.RoundToInt((worldSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((worldSizeY - 1) * percentY);
		return world[x, y];
	}

	//FOR TESTING
	//Drawing the world grid and path
	void OnDrawGizmos(){
		//Base of grid
		Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));

		//If checked in editor...
		if (onlyDisplayPathGizmos) {
			//Only draw path
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube (n.worldPos, Vector3.one * (diameter - .1f));
				}
			}
		} else {
			if (world != null) {
				//Draw obstacles and walkable areas
				foreach (Node n in world) {
					if(n.walkable){
						//WALKABLE = WHITE
						Gizmos.color = Color.white;
					}else{
						//UNWALKABLE = RED
						Gizmos.color = Color.red;
					}

					//Draw path
					if (path != null){
						if (path.Contains(n)){
							Gizmos.color = Color.black;
						}
					}

					Gizmos.DrawCube(n.worldPos, Vector3.one * (diameter - .1f));
				}
			}
		}
	}
}