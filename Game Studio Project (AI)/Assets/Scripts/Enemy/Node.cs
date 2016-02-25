/* Programmer: Kenneth Widemon
 * Description: Node Class that the other scripts reference to access nodes in the world
*/

using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {
	
	public bool walkable;
	public Vector3 worldPos;
	public int worldX;
	public int worldY;
	
	public int gCost;
	public int hCost;
	public Node parent;
	public int heapIndex;
	
	public Node(bool w, Vector3 wP, int x, int y) {
		walkable = w;
		worldPos = wP;
		worldX = x;
		worldY = y;
	}
	
	public int fCost {
		get {
			return gCost + hCost;
		}
	}

	public int HeapIndex {
		get{
			return heapIndex;
		}
		set{
			heapIndex = value;
		}
	}

	//
	public int CompareTo(Node toCompare){
		//Compare fcost of the two nodes
		int compare = fCost.CompareTo (toCompare.fCost);

		//If the two fcosts are equal...
		if (compare == 0){
			//Compare hcosts
			compare = hCost.CompareTo (toCompare.hCost);
		}

		//Normally returns 1 if current item has higher priority than item it's being compared to, but we want to 
		//return 1 if it has a lower priority
		return -compare;
	}
}