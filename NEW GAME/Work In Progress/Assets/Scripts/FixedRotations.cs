using UnityEngine;
using System.Collections;

public class FixedRotations : MonoBehaviour {

    public bool X, Y, Z;
    private float x_rot, y_rot, z_rot;
    private Transform self;

	// Use this for initialization
	void Start () {
        self = gameObject.GetComponent<Transform>();

        x_rot = self.eulerAngles.x;
        y_rot = self.eulerAngles.y;
        z_rot = self.eulerAngles.z;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = self.eulerAngles;

        if (X)
            temp.x = x_rot;
        else
            temp.x = self.eulerAngles.x;
        if (Y)
            temp.y = y_rot;
        else
            temp.y = self.eulerAngles.y;
        if (Z)
            temp.z= z_rot;
        else
            temp.z = self.eulerAngles.z;

        self.rotation = Quaternion.Euler(temp);
    }
}
