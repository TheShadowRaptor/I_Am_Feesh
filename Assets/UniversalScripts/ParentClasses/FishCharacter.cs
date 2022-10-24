using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : GameCharacter
{
    protected Renderer renderer;

    protected float currentAttackTime = 0;
    protected float attackTime = 5;

    protected int damage;

    [Header("StartDir --- (Flase = Right, True = Left)")]
    public bool changeDir;

    [Header("FishStats")]
    public float swimSpeed = 20;
    public float fleeSwimSpeed = 40;

    [Header("Scripts")]
    public DetectPlayer detectPlayer;
    public DetectWarning warningBehaviour;

    [Header("GameObjects")]
    public GameObject food;
    protected GameObject player;

    public bool PlayerSpotted()
    {
        // If player is spotted becomes true
        if (detectPlayer.spottedPlayer)
        {
            return true;
        }
        return false;
    }

    protected void Move()
    {
        //Move
        rb.velocity = transform.right * swimSpeed * Time.deltaTime;

        if (PlayerSpotted() || WarningSpotted())
        {
            // Swim Away 
            transform.right = -player.transform.position + transform.position;
            swimSpeed = fleeSwimSpeed;
        }

        else
        {
            if (changeDir == false) transform.rotation = new Quaternion(0, 0, 0, 0);
            else if (changeDir == true) transform.rotation = new Quaternion(0, 0, 180, 0);
        }
    }

    protected void SpawnFood()
    {
        if (isDead)
        {
            Instantiate(food, transform.position, transform.rotation);
            Debug.Log("Dead");
        }
    }

    protected bool WarningSpotted()
    {
        // Warning Causes surrounding small fish in warning radius to swim away
        if (warningBehaviour.spottedWarning)
        {
            return true;
        }
        return false;
    }
}
