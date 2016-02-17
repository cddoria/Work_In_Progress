using UnityEngine;
using System.Collections;

public class SP_Script : MonoBehaviour {

    public HealthScript hs;
    public RectTransform trans;
    public int maxSP, curSP;

	// Use this for initialization
	void Start () {
        maxSP = hs.sp;
        curSP = hs.sp;
	}
	
	// Update is called once per frame
	void Update () {
        curSP = hs.sp;
        trans.localPosition = ((maxSP - curSP)*(Vector3.left*(trans.rect.width/maxSP)));
	}
}
