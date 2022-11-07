using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralLatcher : FishCharacter
{
    AudioManager audioManager;

    protected Renderer renderer;
    Color spriteColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        renderer = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        damage = 1;

        if (changeDir == false) transform.rotation = new Quaternion(0, 0, 0, 0);
        else if (changeDir == true) transform.rotation = new Quaternion(0, 0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        }
        CamouflageManager();
        AttackManager();
        AttackTimeDrain();
        FlipCharacterModel();
        CheckState();
        SpawnFood();
        Deactivate();
    }

    void FixedUpdate()
    {
        if (renderer.isVisible)
        {
            Move();
        }
    }

    protected new void Move()
    {
        //Move
        rb.velocity = transform.right * swimSpeed * Time.deltaTime;

        if (PlayerSpotted() || WarningSpotted())
        {
            // Swim Away 
            
            swimSpeed = fleeSwimSpeed;
        }

        else
        {
            transform.right = player.transform.position - transform.position;          
        }
    }

    protected new void SpawnFood()
    {
        if (isDead)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(food, transform.position, transform.rotation);
                Debug.Log("Dead");
            }
        }
    }

    private void AttackManager()
    {
        TakeDamage takeDamage = attackRadius.playerTakeDamage;

        // Attack Input
        if (Attacking()) attackRadiusObj.SetActive(true);
        else attackRadiusObj.SetActive(false);

        // Attack Target        
        if (attackRadius.attackPlayer && takeDamage.canTakeDamage)
        {
            takeDamage.health -= damage;
            player.GetComponent<PlayerController>().causeOfDeath = "Fish Food!";
            player.GetComponent<PlayerController>().hit = true;
        }
    }

    bool Attacking()
    {
        StopAttacking();
        if (attackRange.inRange && currentAttackTime == 0)
        {
            currentAttackTime = attackTime;
            attacking = true;
            return false;
        }

        else if (attacking == true && currentAttackTime <= attackingLength)
        {
            audioManager.PlayEnemyBite();
            return true;
        }

        else
        {
            return false;
        }
    }

    void StopAttacking()
    {
        if (currentAttackTime <= 0 || player.GetComponent<PlayerController>().isDead == true)
        {
            attacking = false;
        }
    }

    void CamouflageManager()
    {
        if (PlayerSpotted() == false)
        {
            spriteColor.a = 0.05f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
        }
        else
        {
            spriteColor.a = 1f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerSpotted() == false)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
            {
                if (changeDir == true) changeDir = false;
                else if (changeDir == false) changeDir = true;
            }
        }
    }
}
