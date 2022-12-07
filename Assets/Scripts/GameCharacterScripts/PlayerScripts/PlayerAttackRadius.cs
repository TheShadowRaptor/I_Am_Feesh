using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRadius : MonoBehaviour
{
    public GameObject enemyObj;
    public GameObject foodObj;

    public TakeDamage enemyTakeDamage;
    public TakeDamage foodTakeDamage;
    public FoodCharacter foodScript;

    public bool attackCurrentFish;
    public bool eatCurrentFood;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTakeDamage = enemyObj.GetComponent<TakeDamage>();
            attackCurrentFish = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            foodTakeDamage = foodObj.GetComponent<TakeDamage>();
            eatCurrentFood = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTakeDamage = null;
            attackCurrentFish = false;
        }

        if (other.gameObject.CompareTag("Food"))
        {
            foodTakeDamage = null;
            eatCurrentFood = false;
        }
    }
}
