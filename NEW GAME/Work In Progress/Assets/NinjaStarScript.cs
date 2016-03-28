using UnityEngine;
using System.Collections;

public class NinjaStarScript : MonoBehaviour {

    private Rigidbody rb;
    private Animation anim;
    private Transform trans;
    private Collider col, pCol;

    void Start()
    {
        rb = gameObject.GetComponentInParent<Rigidbody>();
        anim = gameObject.GetComponent<Animation>();
        trans = gameObject.GetComponent<Transform>();
        col = gameObject.GetComponent<Collider>();
        pCol = gameObject.GetComponentInParent<Collider>();
        Physics.IgnoreCollision(col, pCol);
    }

    // Use this for initialization
    void OnCollisionEnter(Collision obj)
    {
        trans.parent = obj.gameObject.transform;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        col.isTrigger = true;
        anim.Stop();
    }
}