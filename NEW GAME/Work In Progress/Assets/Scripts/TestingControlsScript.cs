using UnityEngine;
using System.Collections;

public class TestingControlsScript : MonoBehaviour {

    public PlayerAmmoScript ammo;
    public PlayerHealthScript health;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            health.TakeDamage(5);
        if (Input.GetKeyDown(KeyCode.Z))
            health.HealDamage(3);
        if (Input.GetKeyDown(KeyCode.E))
            ammo.current = ammo.max;
    }
}
