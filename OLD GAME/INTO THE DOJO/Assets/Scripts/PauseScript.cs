using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public bool pause = false;
    public GameObject GeneralUI, PauseCanvas;
    public PlayerMovement PlayerController;
    public ThrowNinjaStar NinjaStarController;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) //escape toggles pause
        {
            pause = !pause;
        }

        if (pause)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

        //if display == true, menu is set active 
        //if display == false, menu is set inactive
        PauseCanvas.SetActive(pause);
        GeneralUI.SetActive(!pause);
        PlayerController.enabled = !pause;
        NinjaStarController.enabled = !pause;
    }
}
