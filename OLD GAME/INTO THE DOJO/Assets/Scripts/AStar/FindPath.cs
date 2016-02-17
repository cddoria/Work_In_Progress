using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For using Lists

public class FindPath : MonoBehaviour {
	
	public Transform trans; //Array of enemy transform
	public Transform target; //Player transform
	
	Decompose world; //Decompose script reference
	
	//Instead of Start() for script referencing
	void Awake() {
		world = GetComponent<Decompose>();
	}
	
	void Update() {
		//Find the path from each enemy to the player every frame
		try
		{
			Find (trans.position, target.position);
		}
		catch
		{

		}
	}
	
	//Find the path (start = enemy ... end = player)
	void Find(Vector3 startPos, Vector3 endPos) {
		Node start = world.FromWorldPoint(startPos);
		Node end = world.FromWorldPoint(endPos);
		
		//List because we have to search through our open set
		List<Node> open = new List<Node>();
		//HashSet because we don't have to worry about searching through the closed list
		HashSet<Node> closed = new HashSet<Node>();
		
		//Adds the start node to open list
		open.Add(start);
		
		//While the open set is NOT empty
		while (open.Count > 0) {
			Node current = open[0];
			
			//Compare the costs of each node in the open list in order to set the next node
			for (int i = 1; i < open.Count; i ++) {
				if (open[i].fCost < current.fCost || open[i].fCost == current.fCost && open[i].hCost < current.hCost) {
					current = open[i];
				}
			}
			
			//Remove the current node from the open list and add to closed list
			open.Remove(current);
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
					
					//Add neighbor
					if (!open.Contains(neighbor)){
						open.Add(neighbor);
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
		world.path1 = reversedPath;
		
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