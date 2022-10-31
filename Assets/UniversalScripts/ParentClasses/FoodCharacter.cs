using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCharacter : GameCharacter
{
    public int evolutionPoints = 1;
    public float staminaPoints = 5.0f;

    AudioManager audioManager;
    // Start is called before the first frame update

    private void Update()
    {
        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        CheckState();
        Deactivate();

        if (isDead)
        {
            audioManager.PlayFoodEaten();
        }
    }
}
