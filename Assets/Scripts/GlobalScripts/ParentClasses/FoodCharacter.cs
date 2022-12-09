using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCharacter : GameCharacter
{
    public int evolutionPoints = 1;
    public float staminaPoints = 15.0f;

    AudioManager audioManager;
    GameObject player;

    SpriteRenderer spriteRenderer;
    Color spriteColor;

    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;        
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();

        InvincibilityFrames();
        CheckState();
        Deactivate();

        if (isDead)
        {
            audioManager.PlayFoodEaten();
            player.GetComponent<PlayerController>().tallyFoodEaten += 1;
            player.GetComponent<PlayerController>().tallyEvoPoints += 1;
        }
    }

    public void InvincibilityFrames()
    {
        if (takeDamage.hit && isDead == false)
        {
            takeDamage.canTakeDamage = false;
            Debug.Log("Invincibility Activated");
            Debug.Log("Health: " + takeDamage.health);
            hitFrameTime -= Time.deltaTime;
            spriteColor.a = 0.2f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
            if (hitFrameTime <= 0)
            {
                Debug.Log("HitFrames");
                hitFrameTime = 0;
                takeDamage.hit = false;
            }
        }

        else if (isDead)
        {
            takeDamage.hit = false;
        }

        else
        {
            hitFrameTime = startHitFrameTime;
            takeDamage.canTakeDamage = true;
            spriteColor.a = 1f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
        }
    }
}
