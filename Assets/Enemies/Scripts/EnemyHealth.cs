using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    // Update is called once per frame
    void Update()
    {
        HealthClamp();
        CheckState();
    }

    bool IsDead()
    {
        if(health == 0)
        {
            return true;
        }
        return false;
    }

    void HealthClamp()
    {
        if(health <= 0)
        {
            health = 0;
        }
    }

    void CheckState()
    {
        if (IsDead()) gameObject.SetActive(false);
    }
}
