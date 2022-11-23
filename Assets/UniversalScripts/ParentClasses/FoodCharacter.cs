using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCharacter : GameCharacter
{
    public int evolutionPoints = 1;
    public float staminaPoints = 15.0f;

    AudioManager audioManager;
    GameObject playerObj;
    PlayerController player;

    TakeDamage takeDamage;
    // Start is called before the first frame update
    private void Start()
    {
        takeDamage = GetComponent<TakeDamage>();    
    }

    private void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }

        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        CheckState();
        Deactivate();

        if (isDead)
        {
            audioManager.PlayFoodEaten();
            playerObj.GetComponent<PlayerController>().tallyFoodEaten += 1;
            playerObj.GetComponent<PlayerController>().tallyEvoPoints += 1;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackRadius"))
        {
            if (player.Attacking() == true)
            {
                takeDamage.health -= 1;
            }
        }
    }
}
