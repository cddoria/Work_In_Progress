using UnityEngine;
using System.Collections;

public class HP_Script : MonoBehaviour {

    public HealthScript hs;
    public RectTransform trans;
    public int maxHealth, curHealth;

	// Use this for initialization
	void Start () {
        maxHealth = hs.hp;
        curHealth = hs.hp;
	}
	
	// Update is called once per frame
	void Update () {
        curHealth = hs.hp;
        trans.localPosition = ((maxHealth - curHealth)*(Vector3.left*(trans.rect.width/maxHealth)));
	}
}
