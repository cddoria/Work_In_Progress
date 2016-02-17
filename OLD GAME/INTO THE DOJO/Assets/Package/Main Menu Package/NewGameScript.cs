using UnityEngine;
using System.Collections;

public class NewGameScript : MonoBehaviour {

    public string level = "Ninja2";
	// Use this for initialization
	void Start () {
        Application.LoadLevel(level);
	}
}
