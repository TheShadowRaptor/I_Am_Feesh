using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public int health;
    public bool canTakeDamage = true;
    void Update()
    {
        HealthClamp();
    }

    void HealthClamp()
    {
        if (health <= 0)
        {
            health = 0;
        }
    }
}
