using UnityEngine;
using System.Collections;

public class NinjaStarScript : MonoBehaviour {

    public float lifespan;
    private Rigidbody rb;
    public Animator anim;
    private bool freeze = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Use this for initialization
    void Update()
    {
        lifespan -= Time.deltaTime;

        if (lifespan < 0f)
            Destroy(this.gameObject);

        if (freeze)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            anim.Stop();
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.detectCollisions = false;
        rb.velocity = Vector3.zero;
        freeze = true;
        this.transform.position = this.transform.TransformPoint(Vector3.forward *.5f);
    }
}