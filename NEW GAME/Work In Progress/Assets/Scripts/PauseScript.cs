using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

    public bool pause = false;
    public RectTransform GeneralUI, PauseCanvas;
    public MoveAndLook moveAndLookScript;
    public ThrowStar throwStarScript;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab)) //escape toggles pause
            pause = !pause;

        if (pause)
            Time.timeScale = 0.0f;
        else
            Time.timeScale = 1.0f;

        //if display == true, menu is set active 
        //if display == false, menu is set inactive
        PauseCanvas.gameObject.SetActive(pause);
        GeneralUI.gameObject.SetActive(!pause);
        moveAndLookScript.enabled = !pause;
        throwStarScript.enabled = !pause;
    }
}
