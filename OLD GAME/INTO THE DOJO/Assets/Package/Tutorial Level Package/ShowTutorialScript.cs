using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowTutorialScript : MonoBehaviour {

    public GameObject text;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.tag == "Player")
            text.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if(obj.tag == "Player")
            text.SetActive(false);
    }
}
