using UnityEngine;
using System.Collections;

public class MoveAndLook : MonoBehaviour
{

    public float LookSensitivity = .02f;
    private Vector3 old, cur, left, right, up, down;

    void Start()
    {
        cur = Input.mousePosition;
        old = Vector3.zero;
        left = this.gameObject.transform.rotation.eulerAngles + Vector3.left;
        right = this.gameObject.transform.rotation.eulerAngles + Vector3.right;
        up = this.gameObject.transform.rotation.eulerAngles + Vector3.up;
        down = this.gameObject.transform.rotation.eulerAngles + Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        // Add Camera Rotation Code Here
        /***********/
        //

        if (Input.GetKey(KeyCode.W))
            this.gameObject.transform.position += new Vector3(0f, 0f, .2f);
        if (Input.GetKey(KeyCode.A))
            this.gameObject.transform.position += new Vector3(-.2f, 0f, 0f);
        if (Input.GetKey(KeyCode.S))
            this.gameObject.transform.position += new Vector3(0f, 0f, -.2f);
        if (Input.GetKey(KeyCode.D))
            this.gameObject.transform.position += new Vector3(.2f, 0f, 0f); ;

        old = cur;
    }
}
