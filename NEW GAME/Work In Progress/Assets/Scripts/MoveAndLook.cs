﻿using UnityEngine;
using System.Collections;

public class MoveAndLook : MonoBehaviour
{

    public Vector2 LookSensitivity = new Vector2(10f, 5f);
    private Transform self, parent;
    private float X_rot, Y_rot;
    private Vector3 direction;
    private Vector3 target;
    public float movementSmoothing = .3f;

    void Start()
    {
        self = gameObject.GetComponent<Transform>();
        parent = gameObject.transform.root.GetComponentInParent<Transform>();
        X_rot = self.localEulerAngles.y;
        Y_rot = self.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        X_rot += Input.GetAxis("Mouse Y") * LookSensitivity.y;
        Y_rot += Input.GetAxis("Mouse X") * LookSensitivity.x;

        X_rot = Mathf.Clamp(X_rot, -45f, 65f);

        if (Y_rot > 360f)
            Y_rot -= 360f;

        self.localEulerAngles = new Vector3(-X_rot, Y_rot, 0f);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        direction = new Vector3(hor, 0f, ver);

        hor = Mathf.Abs(hor);
        ver = Mathf.Abs(ver);

        target = parent.TransformPoint(direction);

        parent.position = Vector3.MoveTowards(parent.position, new Vector3(target.x, parent.position.y, target.z), movementSmoothing*Mathf.Max(hor, ver));
    }
}
