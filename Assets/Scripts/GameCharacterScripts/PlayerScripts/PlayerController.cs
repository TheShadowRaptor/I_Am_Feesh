using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GameCharacter
{
    [Header("BaseStats")]
    public int baseHealth = 1;
    public float baseStamina = 20;
    public int baseDamage = 1;
    public int baseDashCharges = 1;
    public int baseDashSpeed = 2;
    public float baseSwimSpeed = 50;
    public float baseRotateSpeed = 20;
    public float maxDashChargeTime = 5;

    [Header("PlayerStats")]
    public int evolutionPoints;
    public int health;
    public float stamina;
    public int damage;
    public float swimSpeed;
    public float rotateSpeed;
    public int dashCharges;
    public int dashSpeed;
    public float currentDashChargeTime;
    public string currentDepth;

    [Header("Decrease")]
    public float staminaDecrease = 1.0f;
    public float dashSpeedDecrease = 1.0f;
    public float attackTimeDecrease = 5.0f;
    float currentDashSpeed = 1;

    [Header("Increase")]
    public float dashChargeTimeIncrease = 1.0f;

    [Header("Scripts")]
    public PlayerAttackRadius playerAttackRadius;
    public AudioManager audioManager;
    public GameManager gameManager;

    [Header("GameObjects")]
    public GameObject attackRadiusObj;
    public GameObject openMouth;
    public GameObject dashLines;
    public Camera camera;

    // Run Results
    [HideInInspector] public int tallyEvoPoints;
    [HideInInspector] public int tallyKills;
    [HideInInspector] public int tallyFoodEaten;
    [HideInInspector] public string tallyBiome;
    [HideInInspector] public string causeOfDeath;

    // Find Scripts


    // Components
    public Renderer spriteRenderer;
    public Animator animator;
    Color spriteColor;

    // Inputs 
    float horizontalMove;
    float verticalMove;
    bool attackButton;
    bool dashButton;

    Vector3 theScale;

    //Timing
    float currentAttackTime = 0;
    float attackTime = 0.1f;
    float attackLength = 0.05f;
    bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        takeDamage = GetComponent<TakeDamage>();
        spriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        ResetRunStats();
    }

    private void FixedUpdate()
    {
        if (gameManager.state == GameManager.GameState.gameplay)
        {
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        theScale = transform.localScale;

        InputMananger();
        AttackManager();
        InvincibilityFrames();
        AnimationHandler();
        FlipCharacterModel();
        TimerManager();     
        CheckState();     
    }

    protected new void FlipCharacterModel()
    {
        float moveDir = transform.localEulerAngles.z;

        theScale.y = -1;
        if (Mathf.Abs(moveDir) < 90) theScale.y = 1;
        if (Mathf.Abs(moveDir) > 270) theScale.y = 1;
        transform.localScale = theScale;
    }

    void InputMananger()
    {
        //Movement Input
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        //Attack Input
        if (gameManager.state == GameManager.GameState.gameplay)
        {
            attackButton = Input.GetButton("Attack");
        }

        //Dash Input
        dashButton = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1);
    }

    private void Move()
    {
        // Can't move past camera
        Vector3 pos = gameObject.transform.position;
        Vector3 camBounds = new Vector3(camera.orthographicSize * 2, camera.orthographicSize, 0);
        Vector3 camPos = camera.transform.position;
        if (pos.x > camBounds.x + camPos.x) pos.x = camBounds.x + camPos.x;
        if (pos.x < -camBounds.x + camPos.x) pos.x = -camBounds.x + camPos.x;
        if (pos.y > camBounds.y + camPos.y - 3) pos.y = camBounds.y + camPos.y - 3;
        if (pos.y < -camBounds.y + camPos.y + 1) pos.y = -camBounds.y + camPos.y + 1;
        gameObject.transform.position = pos;

        rb.velocity = (Vector2)transform.right * verticalMove * swimSpeed * currentDashSpeed * Time.deltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -horizontalMove * rotateSpeed * Time.deltaTime));

        if (dashButton && dashCharges > 0)
        {
            currentDashSpeed = dashSpeed;
            dashCharges -= 1;
            dashLines.SetActive(true);
            audioManager.PlayPlayerDash();
        }

        //Play Audio
        if (verticalMove > 0 || verticalMove < 0)
        {
            //audioManager.PlayPlayerSwim();
        }
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
        TakeDamage enemyTakeDamage = playerAttackRadius.enemyTakeDamage;
        TakeDamage foodTakeDamage = playerAttackRadius.foodTakeDamage;
        FoodCharacter food = playerAttackRadius.foodScript;
        // Attack Input
        if (Attacking() == true && isDead == false)
        {
            attackRadiusObj.SetActive(true);
            openMouth.SetActive(false);
        }
        else attackRadiusObj.SetActive(false);

        // Attack Target
        if (playerAttackRadius.attackCurrentFish && enemyTakeDamage.hit == false)
        {
            enemyTakeDamage.health -= damage;
            enemyTakeDamage.hit = true;
            playerAttackRadius.attackCurrentFish = false;
        }
        // Consume Food       
        if (playerAttackRadius.eatCurrentFood && foodTakeDamage.hit == false)
        {
            evolutionPoints += food.evolutionPoints;
            stamina += food.staminaPoints;
            foodTakeDamage.health -= damage;
            foodTakeDamage.hit = true;
            playerAttackRadius.eatCurrentFood = false;
        }
    }

    public bool Attacking()
    {
        StopAttacking();
        if (attackButton && currentAttackTime == 0)
        {
            if (gameManager.state == GameManager.GameState.gameplay)
            {
                audioManager.PlayPlayerReadyingBite();
                currentAttackTime = attackTime;
                attacking = true;
                openMouth.SetActive(true);
            }
            return false;
        }

        else if (attacking == true && currentAttackTime <= attackLength)
        {
            if (gameManager.state == GameManager.GameState.gameplay)
            {
                audioManager.PlayPlayerBite();
            }

            return true;
        }

        else
        {
            return false;
        }
    }

    void StopAttacking()
    {
        if (currentAttackTime <= 0 || isDead)
        {
            attacking = false;
        }
    }

    void StaminaDrain()
    {
        stamina -= staminaDecrease * Time.deltaTime;

        // Clamp
        if (stamina <= 0)
        {
            stamina = 0;
            causeOfDeath = "Starved!";
            health = 0;
        }

        if (stamina > baseStamina)
        {
            stamina = baseStamina;
        }
    }

    void DashRecharge()
    {
        // Clamp
        if (currentDashChargeTime <= 0)
        {
            currentDashChargeTime = 0;
        }

        if (currentDashChargeTime > maxDashChargeTime)
        {
            currentDashChargeTime = maxDashChargeTime;
        }

        if (baseDashCharges == 1)
        {
            if (dashCharges == 0)
            {
                currentDashChargeTime += dashChargeTimeIncrease * Time.deltaTime;

                if (currentDashChargeTime >= maxDashChargeTime)
                {
                    dashCharges = 1;
                    currentDashChargeTime = 0;
                }
            }
        }

        else if (baseDashCharges == 2)
        {
            if (dashCharges <= 1)
            {
                currentDashChargeTime += dashChargeTimeIncrease * Time.deltaTime;

                if (currentDashChargeTime >= maxDashChargeTime)
                {
                    dashCharges += 1;
                    currentDashChargeTime = 0;
                }
            }
        }

        else if (baseDashCharges >= 3)
        {
            if (dashCharges <= 2)
            {
                currentDashChargeTime += dashChargeTimeIncrease * Time.deltaTime;

                if (currentDashChargeTime == maxDashChargeTime)
                {
                    dashCharges += 1;
                    currentDashChargeTime = 0;
                }
            }
        }
    }

    void DashDrain()
    {
        currentDashSpeed -= dashSpeedDecrease * Time.deltaTime;

        // Clamp
        if (currentDashSpeed <= 1)
        {
            dashLines.SetActive(false);
            currentDashSpeed = 1;
        }

        if (currentDashSpeed > dashSpeed)
        {
            currentDashSpeed = dashSpeed;
        }
    }

    void AttackTimeDrain()
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

    void TimerManager()
    {
        StaminaDrain();
        DashRecharge();
        DashDrain();
        AttackTimeDrain();
    }

    public void ResetRunStats()
    {
        health = baseHealth;
        takeDamage.health = health;
        stamina = baseStamina;
        damage = baseDamage;
        swimSpeed = baseSwimSpeed;
        rotateSpeed = baseRotateSpeed;
        dashCharges = baseDashCharges;
        dashSpeed = baseDashSpeed;

        currentDashChargeTime = 0;

        tallyEvoPoints = 0;
        tallyFoodEaten = 0;
        tallyKills = 0;
        tallyBiome = "Shallow";

        openMouth.SetActive(false);
    }

    public void GodMode()
    {
        evolutionPoints = 999;
        health = 999;
        takeDamage.health = 999;
        staminaDecrease = 0;
        damage = 999;
        swimSpeed = 500;
        rotateSpeed = 200;
        dashCharges = 999;
        dashSpeed = 999;
    }

    public void FullyResetStats()
    {
        evolutionPoints = 0;
        baseHealth = 1;
        baseStamina = 20;
        baseDamage = 1;
        baseDashCharges = 1;
        baseDashSpeed = 2;
        baseSwimSpeed = 50;
        baseRotateSpeed = 20;

        health = baseHealth;
        takeDamage.health = health;
        stamina = baseStamina;
        damage = baseDamage;
        swimSpeed = baseSwimSpeed;
        rotateSpeed = baseRotateSpeed;
        dashCharges = baseDashCharges;
        dashSpeed = baseDashSpeed;
    }

    public new void CheckState()
    {
        if (health <= 0 || takeDamage.health <= 0 || health <= 0 && takeDamage.health <= 0) 
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
        health = takeDamage.health;
    }

    void AnimationHandler()
    {
        if (verticalMove > 0 || verticalMove < 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
