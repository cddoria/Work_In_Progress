using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public bool pause = false;
    public bool display = false;
    public GameObject menu;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) //escape toggles pause
        {
            pause = !pause;
            display = pause;
        }

        if (pause)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

        menu.SetActive(display); //if display == true, menu is set active 
                                 //if display == false, menu is set inactive
	}
}
