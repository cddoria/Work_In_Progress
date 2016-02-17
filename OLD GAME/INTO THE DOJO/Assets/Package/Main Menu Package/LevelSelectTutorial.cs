using UnityEngine;
using System.Collections;

public class LevelSelectTutorial : MonoBehaviour {

    public string level = "Ninja";
	// Use this for initialization
	void Start () {
        Application.LoadLevel(level);
	}
}
