using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float health;

    void Update()
    {
        HealthClamp();
    }

    bool IsDead()
    {
        if (health == 0)
        {
            return true;
        }
        return false;
    }

    void HealthClamp()
    {
        if (health <= 0)
        {
            health = 0;
        }
    }
}
