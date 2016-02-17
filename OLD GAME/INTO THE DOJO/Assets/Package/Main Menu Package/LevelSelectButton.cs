using UnityEngine;
using System.Collections;

public class LevelSelectButton : MonoBehaviour {

    public string level = "LevelSelect";
	// Use this for initialization
	void Start () {
	    Application.LoadLevel(level);
	}
}