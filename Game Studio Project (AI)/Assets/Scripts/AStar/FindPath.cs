/* Programmer: Kenneth Widemon
 * Description: This script is attached to an empty gameobject called AStar with the Decompose script
 * 				and finds the best path for the Enemy to traverse in order to reach a target.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For using List and Heap

public class FindPath : MonoBehaviour {
	public List<Node> path; //Path of Nodes
	Decompose decompScript; //Decompose script reference
	
	//Instead of Start() for script referencing
	void Awake() {
		decompScript = GetComponent<Decompose> (); //Gets script from this object
	}
	
	//Find the path (start = enemy ... end = player)
	public void Find(Vector3 startPos, Vector3 endPos) {
		//Get the world position for the enemy and player
		Node start = decompScript.FromWorldPoint(startPos);
		Node end = decompScript.FromWorldPoint(endPos);
		
		//Heap for better performance when searching through nodes
		//Open set is the set of nodes to be evaluated
		Heap<Node> open = new Heap<Node>(decompScript.MaxSize);
		//HashSet because we don't have to worry about searching through the closed set
		//Closed set is the set of nodes that have already been evaluated
		HashSet<Node> closed = new HashSet<Node>();
		
		//Adds the start node to open set
		open.Add(start);
		
		//While the open set is NOT empty...
		while (open.Count > 0) {
			//Remove the current node from the open set
			Node current = open.RemoveFirst ();
			//Add to the closed set
			closed.Add(current);
			
			//If current node equals final node...
			if (current == end) {
				//Retrace to create the path
				Retrace(start, end);
				return;
			}
			
			//Check the nodes in the neighbors list
			foreach (Node neighbor in decompScript.Neighbors(current)) {
				//Skip node if UNWALKABLE or if in the closed list
				if (!neighbor.walkable || closed.Contains(neighbor)) {
					continue;
				}
				
				//New cost equals the current node's gCost plus the distance between the current node and neighbor
				int newCostToNeighbor = current.gCost + Distance(current, neighbor);
				
				//Compare new cost to that of the neighbors
				if (newCostToNeighbor < neighbor.gCost || !open.Contains(neighbor)) {
					//Set new gCost of neighbor
					neighbor.gCost = newCostToNeighbor;
					
					//Set new hCost of neighbor
					neighbor.hCost = Distance(neighbor, end);
					
					//Set the parent of the neighbor as the current node
					neighbor.parent = current;
					
					//If not already in the open set
					if (!open.Contains (neighbor)) {
						//Add neighbor
						open.Add (neighbor);
					} else {
						//Update the sort of the item in the heap
						open.UpdateItem (neighbor);
					}
				}
			}
		}
	}
	
	//Retrace to create path
	void Retrace(Node start, Node end) {
		List<Node> reversedPath = new List<Node>(); //New list for reversed path nodes
		Node current = end; //Start at the end
		
		//While not at the original start node...
		while (current != start) {
			//Add nodes to reversed path List
			reversedPath.Add(current);
			//Set that node's parent to the current node
			current = current.parent;
		}
		
		//Reverse to create correct path
		reversedPath.Reverse();
		
		//Path = Reversed Path
		path = reversedPath;
		
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