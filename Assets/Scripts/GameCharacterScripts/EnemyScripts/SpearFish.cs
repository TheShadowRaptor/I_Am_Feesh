using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearFish : FishCharacter
{
    public Renderer spriteRenderer;
    // Components
    Color spriteColor;
    AudioManager audioManager;

    GameObject camObj;
    CameraClamp cameraScroll;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        player = GameObject.Find("Player");
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();

        if (camObj == null)
        {
            camObj = GameObject.Find("MainCamera");
            cameraScroll = camObj.GetComponent<CameraClamp>();
        }

        if (spriteRenderer.isVisible) LockCamera();
        else isDead = true;

        if (isDead) UnlockCamera();
        AttackManager();
        AttackTimeDrain();
        FlipCharacterModel();
        SpawnFood();
        Deactivate();
        CheckState();
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


        if (changeDir == false) transform.rotation = new Quaternion(0, 0, 0, 0);
     else if (changeDir == true) transform.rotation = new Quaternion(0, 0, 180, 0);

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

    }

    public void InvincibilityFrames()
    {
        if (takeDamage.hit && isDead == false)
        {
            hitFrameTime -= Time.deltaTime;
            takeDamage.canTakeDamage = false;
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

        // Attack Target        
        if (attackRadius.attackPlayer && takeDamage.canTakeDamage && player.GetComponent<PlayerController>().isDead == false)
        {
            takeDamage.health -= damage;
            player.GetComponent<PlayerController>().causeOfDeath = "Fish Food!";
            audioManager.PlayEnemyBite();
            takeDamage.hit = true;
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
