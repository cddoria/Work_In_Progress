using UnityEngine;
using System.Collections;

public class NinjaStarLifespan : MonoBehaviour {

    private float start;
    public float lifeSpan;

	// Use this for initialization
	void Start () {
        start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - start > lifeSpan)
            GameObject.Destroy(this.gameObject);
	}
}
