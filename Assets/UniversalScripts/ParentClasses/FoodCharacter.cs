using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCharacter : GameCharacter
{
    public int evolutionPoints = 1;
    public float staminaPoints = 5.0f;

    AudioManager audioManager;
    GameObject player;
    // Start is called before the first frame update

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        CheckState();
        Deactivate();

        if (isDead)
        {
            audioManager.PlayFoodEaten();
            player.GetComponent<PlayerController>().tallyFoodEaten += 1;
            player.GetComponent<PlayerController>().tallyEvoPoints += 1;
        }
    }
}
