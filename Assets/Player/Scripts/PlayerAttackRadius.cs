using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackRadius : MonoBehaviour
{
    public GameObject enemyObj;
    public EnemyHealth enemyHealthScript;
    public bool attacking;
    private void OnTriggerStay2D(Collider2D other)
    {  
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyObj = other.gameObject;
            enemyHealthScript = enemyObj.GetComponent<EnemyHealth>();
            attacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyObj = null;
            enemyHealthScript = null;
            attacking = false;
        }
    }
}
