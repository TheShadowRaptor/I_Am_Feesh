using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    public PlayerAttackRadius attackRadius;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            attackRadius.enemyObj = other.gameObject;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            attackRadius.foodObj = other.gameObject;
            attackRadius.foodScript = attackRadius.foodObj.GetComponent<FoodCharacter>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            attackRadius.enemyObj = null;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            attackRadius.foodObj = null;
            attackRadius.foodScript = null;
        }
    }
}
