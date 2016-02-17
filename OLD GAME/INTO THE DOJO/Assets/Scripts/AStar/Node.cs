using UnityEngine;
using System.Collections;

public class Node {
	
	public bool walkable;
	public Vector2 worldPos;
	public int worldX;
	public int worldY;
	
	public int gCost;
	public int hCost;
	public Node parent;
	
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
}
