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
            enemyObj = other.gameObject;
            takeDamage = enemyObj.GetComponent<TakeDamage>();
            attackCurrentFish = true;
        }

        else if (other.gameObject.CompareTag("Food"))
        {
            foodObj = other.gameObject;
            foodScript = foodObj.GetComponent<FoodCharacter>();
            takeDamage = foodObj.GetComponent<TakeDamage>();
            eatCurrentFood = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyObj = null;
            takeDamage = null;
            attackCurrentFish = false;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.CompareTag("Food"))
            {
                foodObj = null;
                foodScript = null;
                takeDamage = null;
                eatCurrentFood = false;
            }
        }
    }
}
