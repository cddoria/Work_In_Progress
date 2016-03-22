using UnityEngine;
using System.Collections;

public class MoveAndLook : MonoBehaviour
{

    public Vector2 LookSensitivity = new Vector2(10f, 5f);
    private Transform self;
    private float X_rot, Y_rot;
    private Vector3 direction;
    private Vector3 target;
    private float movementSmoothing = .3f;

    void Start()
    {
        self = gameObject.GetComponent<Transform>();
        X_rot = self.localEulerAngles.y;
        Y_rot = self.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        X_rot += Input.GetAxis("Mouse Y") * LookSensitivity.y;
        Y_rot += Input.GetAxis("Mouse X") * LookSensitivity.x;

        X_rot = Mathf.Clamp(X_rot, -65f, 65f);

        if (Y_rot > 360f)
            Y_rot -= 360f;

        self.localEulerAngles = new Vector3(-X_rot, Y_rot, 0f);

        direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        target = transform.position + direction;

        transform.position = Vector3.MoveTowards(transform.position, target, movementSmoothing);
    }
}
