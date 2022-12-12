using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoralLatcher : FishCharacter
{
    public Renderer spriteRenderer;
    public GameObject openMouth;
    // Components
    Color spriteColor;
    AudioManager audioManager;

    GameObject camObj;
    CameraClamp cameraScroll;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        damage = 1;

        if (changeDir == false) transform.rotation = new Quaternion(0, 0, 0, 0);
        else if (changeDir == true) transform.rotation = new Quaternion(0, 0, 180, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;

        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        }

        if (camObj == null)
        {
            camObj = GameObject.Find("MainCamera");
            cameraScroll = camObj.GetComponent<CameraClamp>();
        }

        if (isDead) UnlockCamera();
        InvincibilityFrames();
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
        if (spriteRenderer.isVisible)
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

            transform.right = player.transform.position - transform.position;
            swimSpeed = fleeSwimSpeed;
            LockCamera();
        }

        else
        {           
            transform.right = player.transform.position - transform.position;          
        }
    }

    public void LockCamera()
    {
        cameraScroll.isEnabled = false;
    }

    public void UnlockCamera()
    {
        cameraScroll.isEnabled = true;
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

    public void InvincibilityFrames()
    {
        if (takeDamage.hit && isDead == false)
        {
            takeDamage.canTakeDamage = false;
            hitFrameTime -= Time.deltaTime;
            spriteColor.a = 0.2f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
            if (hitFrameTime <= 0)
            {
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

    private void AttackManager()
    {
        TakeDamage takeDamage = attackRadius.playerTakeDamage;

        // Attack Input
        if (Attacking())
        {
            attackRadiusObj.SetActive(true);
            openMouth.SetActive(false);
        }
        else attackRadiusObj.SetActive(false);

        // Attack Target        
        if (attackRadius.attackPlayer && takeDamage.canTakeDamage)
        {
            takeDamage.health -= damage;
            player.GetComponent<PlayerController>().causeOfDeath = "Fish Food!";
            takeDamage.hit = true;
        }
    }

    bool Attacking()
    {
        StopAttacking();
        if (attackRange.inRange && currentAttackTime == 0)
        {
            currentAttackTime = attackTime;
            openMouth.SetActive(true);
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
            spriteColor.a = 0.02f;
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
