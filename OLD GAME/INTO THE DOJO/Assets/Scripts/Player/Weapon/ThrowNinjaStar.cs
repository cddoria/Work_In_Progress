using UnityEngine;
using System.Collections;

public class ThrowNinjaStar : MonoBehaviour {

	public GameObject prefab;
	public HealthScript hs;
    private GameObject ninjaStar;
	public int throwCost;

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if(hs.sp > throwCost)
			{
				ninjaStar = Instantiate(prefab).gameObject as GameObject;
            	Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), ninjaStar.GetComponent<CircleCollider2D>());
				ninjaStar.transform.position = transform.position;
            	ninjaStar.tag = "NinjaStar";
            	NinjaStarVelocity nsv = ninjaStar.GetComponent<NinjaStarVelocity>();
            	nsv.setTargetDirection(Input.mousePosition);
				hs.sp -= throwCost;
			}
		}
	}
}
