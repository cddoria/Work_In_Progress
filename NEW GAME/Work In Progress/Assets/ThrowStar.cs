using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ThrowStar : MonoBehaviour
{
    public Transform trans;
    public GameObject starPrefab;
    private Rigidbody starRB;

    // Use this for initialization
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject temp = Instantiate(starPrefab);
            temp.transform.position = trans.TransformPoint(Vector3.right*1f);
            temp.transform.rotation.SetLookRotation(temp.transform.TransformPoint(Vector3.forward));
            starRB = temp.GetComponent<Rigidbody>();
            starRB.useGravity = false;
            starRB.AddForce((Vector3.left+Vector3.down) * 75f);
            starRB.AddForce(trans.forward*1000f);
            starRB.gameObject.GetComponentInParent<Transform>().rotation = Quaternion.LookRotation(trans.forward);
            Destroy(temp, 3f);
        }
    }
}