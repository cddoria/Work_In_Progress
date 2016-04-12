using UnityEngine;
using System.Collections;

public class PlayerAmmoScript : MonoBehaviour {

    public int current;
    public int max;
    public float cooldown;
    public float refillTime;

    // Update is called once per frame
    void Start ()
    {
        cooldown = 0;
        current = 0;
    }

    void Update () {
        cooldown += Time.deltaTime;

        if (cooldown > refillTime)
            if (current < max)
            {
                current += 1;
                cooldown = 0f;
            }

        if (cooldown > refillTime)
            cooldown = refillTime;
	}

    public void SpendAmmo (int count)
    {
        if(current >= count)
            current -= count;
    }
}
