using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRadius : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject foodObj;

    public TakeDamage takeDamage;
    public FoodCharacter foodScript;

    public bool attackCurrentFish;
    public bool eatCurrentFood;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            takeDamage = enemyObj.GetComponent<TakeDamage>();
            attackCurrentFish = true;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            takeDamage = foodObj.GetComponent<TakeDamage>();
            eatCurrentFood = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            takeDamage = null;
            attackCurrentFish = false;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            takeDamage = null;
            eatCurrentFood = false;
        }
    }
}
