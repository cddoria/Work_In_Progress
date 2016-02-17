using UnityEngine;
using System.Collections;

public class SfxScript : MonoBehaviour {

    public AudioSource sound;
    public AudioClip[] sfxList;
    public int sfxSelector;

	// Use this for initialization
	void Start () {
        sound = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (sfxSelector >= 0 && sfxSelector < sfxList.Length) {
            sound.clip = sfxList[sfxSelector];
            sound.Play();
            sfxSelector = -1;
        }
	}
}
