using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuControllerScript : MonoBehaviour {

    public int CurrentMenu = 0;
    public Canvas[] MenuCanvases;
	
    void Update()
    {
        foreach(Canvas c in MenuCanvases)
            c.enabled = false;

        MenuCanvases[CurrentMenu].enabled = true;
    }
	// Update is called once per frame
	public void SetMenu (int sel) {
        if (sel != CurrentMenu)
        CurrentMenu = sel;
	}

    public void LoadLevel(string level) {
        SceneManager.LoadScene(level);
    }

    public void Quit() {
        Application.Quit();
    }
}
