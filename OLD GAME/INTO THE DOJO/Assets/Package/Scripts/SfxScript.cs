using UnityEngine;
using System.Collections;

public class sfxScript : MonoBehaviour {

    private AudioSource sound;
    public AudioClip[] sfxList;
    public int sfxSelector;

	// Use this for initialization
	void Start () {
        sound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (sfxSelector >= 0 && sfxSelector < sfxList.Length) {
            sound.Stop();
            sound.clip = sfxList[sfxSelector];
            sound.Play();
            sfxSelector = -1;
        }
	}
}