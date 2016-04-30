/* Programmer: Kenneth Widemon
 * Description: Node Class that the other scripts reference to access nodes in the heap
*/

using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
	
	public bool walkable; //Whether each node is walkable
	public Vector3 worldPos; //Each node's world position
	public int worldX;
	public int worldY;
	
	public int gCost; //Base score; Cost of moving from start node to each node
	public int hCost; //Heuristic: Estimate of the distance between each node and goal node
	public Node parent; //Parent of each node
	public int heapIndex; //Index of each node in the heap
	
	public Node(bool w, Vector3 wP, int x, int y) {
		walkable = w;
		worldPos = wP;
		worldX = x;
		worldY = y;
	}

	//Determines and returns the node's fCost; The total cost of the path from each node
	public int fCost {
		get {
			return gCost + hCost;
		}
	}

	//Getter and Setter for heap index
	public int HeapIndex {
		get{
			return heapIndex;
		}
		set{
			heapIndex = value;
		}
	}

	//Compare nodes to determine priority
	public int CompareTo(Node toCompare){
		//Compare fcost of the two nodes
		int compare = fCost.CompareTo (toCompare.fCost);

		//If the two fcosts are equal...
		if (compare == 0){
			//Compare hcosts
			compare = hCost.CompareTo (toCompare.hCost);
		}

		/*
		 * Normally returns 1 if current item has higher priority than item it's being compared to, but we want to 
		 * return 1 if it has a lower priority
		*/
		return -compare;
	}
}