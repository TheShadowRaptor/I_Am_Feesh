using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRadius : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject foodObj;

    public characterHealth characterHealth;
    public FoodCharacter foodScript;

    public bool attackCurrentFish;
    public bool eatCurrentFood;
    private void OnTriggerStay2D(Collider2D other)
    {  
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyObj = other.gameObject;
            characterHealth = enemyObj.GetComponent<characterHealth>();
            attackCurrentFish = true;
        }

        else if (other.gameObject.CompareTag("Food"))
        {
            foodObj = other.gameObject;
            foodScript = foodObj.GetComponent<FoodCharacter>();
            characterHealth = foodObj.GetComponent<characterHealth>();
            eatCurrentFood = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyObj = null;
            characterHealth = null;
            attackCurrentFish = false;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            if (other.gameObject.CompareTag("Food"))
            {
                foodObj = null;
                foodScript = null;
                characterHealth = null;
                eatCurrentFood = false;
            }
        }
    }
}
