using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour
{
    public GameObject player;

    public TakeDamage playerTakeDamage;

    public bool attackPlayer;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            playerTakeDamage = player.GetComponent<TakeDamage>();
            attackPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            playerTakeDamage = null;
            attackPlayer = false;
        }
    }
}
