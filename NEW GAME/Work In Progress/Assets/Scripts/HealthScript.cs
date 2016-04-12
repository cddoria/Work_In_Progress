using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public int health = 20;

    void OnCollisionEnter(Collision obj)
    {
        TakeDamage(obj.gameObject.tag);
        if (obj.gameObject.tag == "Projectile") Destroy(obj.gameObject.transform.root.gameObject);
        CheckForDeath();
    }

    void TakeDamage(string type)
    {
        switch(type)
        {
            case "Projectile":
                health -= 5;
                break;
            case "Weapon":
                health -= 10;
                break;
        }
    }

    void CheckForDeath()
    {
        if (health <= 0) Destroy(this.gameObject);
    }
}
