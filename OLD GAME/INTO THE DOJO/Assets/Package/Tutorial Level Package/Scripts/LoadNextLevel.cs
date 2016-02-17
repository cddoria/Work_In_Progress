using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadNextLevel : MonoBehaviour {

    public List<GameObject> EnemyList = new List<GameObject>();
    public string NextLevel;

	void OnTriggerEnter2D(Collider2D obj) {
		if (obj.tag == "Player" && EnemyList.Count == 0) {
			Debug.Log ("Loading Next Level");
			Application.LoadLevel(NextLevel);
		}
	}
}
