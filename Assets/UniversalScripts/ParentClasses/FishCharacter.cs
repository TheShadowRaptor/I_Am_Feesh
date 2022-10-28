using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : GameCharacter
{
    protected GameObject player;

    protected float attackTime = 0.2f;


    protected bool attacking = false;


    protected int damage;

    [Header("StartDir --- (Flase = Right, True = Left)")]
    public bool changeDir;

    [Header("FishStats")]
    public float swimSpeed = 20;
    public float fleeSwimSpeed = 40;

    public float attackingLength = 0.1f;
    public float currentAttackTime = 0;
    public float attackTimeDecrease = 5.0f;

    [Header("Scripts")]
    public DetectPlayer detectPlayer;
    public DetectWarning warningBehaviour;

    [Header("GameObjects")]
    public GameObject food;

    public EnemyAttackRange attackRange;
    public EnemyAttackRadius attackRadius;
    public GameObject attackRadiusObj;

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

    protected void AttackTimeDrain()
    {
        currentAttackTime -= attackTimeDecrease * Time.deltaTime;

        // Clamp
        if (currentAttackTime <= 0)
        {
            currentAttackTime = 0;
        }

        if (currentAttackTime > attackTime)
        {
            currentAttackTime = attackTime;
        }
    }
}
