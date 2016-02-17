using UnityEngine;
using System.Collections;
using System.Collections.Generic; //To use Lists

public class Decompose : MonoBehaviour {
	
	public List<Node> path1; //List for nodes in path1
	public List<Node> path2; //List for nodes in path2
	public LayerMask unwalkable; //Layermask for obstacles
	public Vector2 worldSize; //Size of the world of nodes
	public float radius; //Node radius
	
	Node[,] world; //2D array of nodes in world
	
	private float diameter; //Node diameter
	private int worldSizeX, worldSizeY; //X and Y coordinates of world
	
	//Initialize the world dimensions
	void Start() {
		diameter = radius * 2;
		worldSizeX = Mathf.RoundToInt(worldSize.x / diameter);
		worldSizeY = Mathf.RoundToInt(worldSize.y / diameter);
		DecomposeWorld();
	}
	
	//Create world
	void DecomposeWorld() {
		world = new Node[worldSizeX, worldSizeY];
		
		Vector3 bottomLeft = transform.position - Vector3.right * (worldSize.x / 2) - Vector3.up* (worldSize.y / 2);
		
		for (int x = 0; x < worldSizeX; x++) {
			for (int y = 0; y < worldSizeY; y++) {
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * diameter + radius) + Vector3.up * (y * diameter + radius);
				
				bool walkable = false;
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
	
	//Access the world point for each node in path
	public Node FromWorldPoint(Vector3 worldPos) {
		float percentX = (worldPos.x + worldSize.x / 2) / worldSize.x;
		float percentY = (worldPos.y + worldSize.y / 2) / worldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		
		int x = Mathf.RoundToInt((worldSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((worldSizeY - 1) * percentY);
		return world[x, y];
	}
	
	//FOR TESTING
	//Draw the world grid and path when "Gizmos" are turned on
	void OnDrawGizmos(){
		//Base of grid
		Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, worldSize.y, 0));
		
		if (world != null) {
			foreach (Node n in world) {
				if(n.walkable){
					//WALKABLE = WHITE
					Gizmos.color = Color.white;
				}else{
					//UNWALKABLE = RED
					Gizmos.color = Color.red;
				}
				
				if (path1 != null){
					if (path1.Contains(n)){
						Gizmos.color = Color.black;
					}
				}

				if (path2 != null){
					if (path2.Contains(n)){
						Gizmos.color = Color.black;
					}
				}

				Gizmos.DrawCube(n.worldPos, Vector3.one * (diameter - .1f));
			}
		}
	}
}