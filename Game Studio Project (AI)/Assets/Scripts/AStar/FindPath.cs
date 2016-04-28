/* Programmer: Kenneth Widemon
 * Description: This script is attached to an empty gameobject called AStar with the Decompose script
 * 				and finds the best path for the Enemy to traverse in order to reach a target.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For using List and Heap

public class FindPath : MonoBehaviour {
	
	public Transform trans; //Array of enemy transform
	public Transform target; //Player transform
	
	Decompose world; //Decompose script reference
	
	//Instead of Start() for script referencing
	void Awake() {
		world = GetComponent<Decompose>();
	}
	
	void Update() {
		//Find the path from the enemy to the player every frame
		Find (trans.position, target.position);
	}
	
	//Find the path (start = enemy ... end = player)
	void Find(Vector3 startPos, Vector3 endPos) {
		Node start = world.FromWorldPoint(startPos);
		Node end = world.FromWorldPoint(endPos);
		
		//Heap for better performance when searching through nodes
		Heap<Node> open = new Heap<Node>(world.MaxSize);
		//HashSet because we don't have to worry about searching through the closed set
		HashSet<Node> closed = new HashSet<Node>();
		
		//Adds the start node to open set
		open.Add(start);
		
		//While the open set is NOT empty
		while (open.Count > 0) {
			//Remove the current node from the open set and add to closed set 
			Node current = open.RemoveFirst ();
			closed.Add(current);
			
			//If current node equals final node, retrace to create the path
			if (current == end) {
				Retrace(start, end);
				return;
			}
			
			//Check the nodes in the neighbors list
			foreach (Node neighbor in world.Neighbors(current)) {
				//Skip node if UNWALKABLE or if in the closed list
				if (!neighbor.walkable || closed.Contains(neighbor)) {
					continue;
				}
				
				//Add the distance between the current node and neighbor to the current node's gCost
				int newCostToNeighbor = current.gCost + Distance(current, neighbor);
				
				//Compare gCosts of neighbors to the new gCost
				if (newCostToNeighbor < neighbor.gCost || !open.Contains(neighbor)) {
					//Set new gCost of neighbor
					neighbor.gCost = newCostToNeighbor;
					
					//Set new hCost of neighbor
					neighbor.hCost = Distance(neighbor, end);
					
					//Set the parent of the neighbor as the current node
					neighbor.parent = current;
					
					//Add neighbor if not already in the open set
					if (!open.Contains (neighbor)) {
						open.Add (neighbor);
					} else {
						open.UpdateItem (neighbor);
					}
				}
			}
		}
	}
	
	//Retrace to create path
	void Retrace(Node start, Node end) {
		List<Node> reversedPath = new List<Node>();
		Node current = end;
		
		//Add nodes to path List until it reaches the start node
		while (current != start) {
			reversedPath.Add(current);
			current = current.parent;
		}
		
		//Reverse to create
		reversedPath.Reverse();
		
		//Path = Reversed Path
		world.path = reversedPath;
		
	}
	
	//Get the distance between the current and neighboring nodes
	int Distance(Node a, Node b) {
		int distanceX = Mathf.Abs(a.worldX - b.worldX);
		int distanceY = Mathf.Abs(a.worldY - b.worldY);
		
		if (distanceX > distanceY) {
			return 14 * distanceY + 10 * (distanceX - distanceY);
		} else {
			return 14 * distanceX + 10 * (distanceY - distanceX);
		}
	}
}