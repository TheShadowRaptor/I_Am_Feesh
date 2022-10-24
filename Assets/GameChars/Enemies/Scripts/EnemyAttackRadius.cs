using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour
{
    public GameObject player;

    public TakeDamage takeDamage;

    public bool attackPlayer;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            takeDamage = player.GetComponent<TakeDamage>();
            attackPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            takeDamage = null;
            attackPlayer = false;
        }
    }
}
