/* Programmer: Kenneth Widemon
 * Description: Heap Class that the other scripts reference to search nodes in the world
*/

using UnityEngine;
using System.Collections;
using System;

public class Heap<Type> where Type : IHeapItem<Type> {

	Type[] items; //Array of items(nodes)
	int currentItemCount; //Counter

	//Constructor : Creates a maximum size for the heap because an array is difficult to resize
	public Heap(int maxHeapSize){
		items = new Type[maxHeapSize];
	}

	//Add new items to heap
	public void Add(Type item){
		//Create the item index
		item.HeapIndex = currentItemCount;
		//Add item to end of array
		items [currentItemCount] = item;

		//Sort items
		SortUp (item);
		currentItemCount++;
	}

	//Remove the first item from heap
	public Type RemoveFirst(){
		//Initialize first item
		Type firstItem = items [0];
		currentItemCount--;

		//Move item at the end of heap to the first slot
		items [0] = items [currentItemCount];
		items [0].HeapIndex = 0;

		//Sort items
		SortDown (items [0]);

		return firstItem;
	}

	//Change priority of item with lower f cost if new path has been found to it (updates position in heap)
	public void UpdateItem(Type item){
		//In path finding, we only increase priority, never decrease it
		SortUp (item);
	}

	//Return number of items currently in heap
	public int Count{
		get{
			return currentItemCount;
		}
	}

	//Check for specific items
	public bool Contains(Type item){
		//Check if item in array with same index as the item being passed in is equal to the actual item being passed in
		return Equals(items[item.HeapIndex], item);
	}

	void SortDown(Type item){
		while (true) {
			//Left Child Index : 2n + 1
			int childLeftIndex = item.HeapIndex * 2 + 1;
			//Right Child Index : 2n + 2
			int childRightIndex = item.HeapIndex * 2 + 2;
			//Index of item to be swapped
			int swapIndex = 0;

			//If item has at least one child...
			if (childLeftIndex < currentItemCount) {
				swapIndex = childLeftIndex;

				//If item has a second child...
				if (childRightIndex < currentItemCount) {
					//Compare children to determine higher priority ; If left child has lower priority than right child...
					if (items [childLeftIndex].CompareTo (items [childRightIndex]) < 0) {
						swapIndex = childRightIndex;
					}
				}

				//Compare parent to highest priority child to determine higher priority ; 
				//If parent has lower priority than highest priority child...
				if (item.CompareTo (items [swapIndex]) < 0) {
					Swap (item, items [swapIndex]);
				} else {
					return;
				}
			} else {
				//Exit loop if parent has no children
				return;
			}
		}
	}

	//Sort comparable items until order is correct
	void SortUp(Type item){
		//Parent Index : (n-1) / 2
		int parentIndex = (item.HeapIndex - 1) / 2;

		//Compare items
		while(true){
			Type parentItem = items[parentIndex];

			//If item has higher priority than the parent...
			if (item.CompareTo (parentItem) > 0) {
				Swap (item, parentItem);
			} else {
				break;
			}

			//Otherwise, keep recalculating the parent index and comparing the item to its parent
			parentIndex = (item.HeapIndex - 1) / 2;
		}
	}

	//Swaps two items
	void Swap(Type itemA, Type itemB){
		items [itemA.HeapIndex] = itemB;
		items [itemB.HeapIndex] = itemA;

		//Swap heap index values
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

/*
 * Interface : to keep track of item indexes and compare items to determine higher priority
 * IComparable : defines a generalized type-specific comparison method that allows a class or value type to sort its instances
 */
public interface IHeapItem<Type> : IComparable<Type>{
	int HeapIndex {
		get;
		set;
	}


}