using UnityEngine;
using System.Collections;

public class PlayerHealthScript : MonoBehaviour {

    public float max;
    public float current;

    public void TakeDamage(float x)
    {
        if(current > 0)
            current -= 5;
        if (current < 0)
            current = 0;
    }

    public void HealDamage(float z)
    {
        if (current < max)
            current += z;
        if (current > max)
            current = max;
    }
}
