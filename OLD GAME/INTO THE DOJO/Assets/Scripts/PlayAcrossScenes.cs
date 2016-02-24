using UnityEngine;
using System.Collections;

public class PlayAcrossScenes : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("BackgroundMusic");
        if(music.Length > 1)
            foreach (GameObject obj in music)
                if (this.gameObject != obj)
                    if (this.gameObject.GetComponent<AudioSource>().clip == obj.GetComponent<AudioSource>().clip)
                        GameObject.Destroy(this.gameObject);
                    else
                        GameObject.Destroy(obj);

        DontDestroyOnLoad(this.gameObject);
    }
}
