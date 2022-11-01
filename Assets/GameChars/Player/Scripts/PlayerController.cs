using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GameCharacter
{
    public bool hit = false;

    [Header("BaseStats")]
    public int baseHealth = 1;
    public float baseStamina = 20;
    public int baseDamage = 1;
    public int baseDashCharges = 1;
    public int baseDashSpeed = 2;
    public float baseSwimSpeed = 50;
    public float baseRotateSpeed = 20;
    public int baseDepthLimit = 0;

    [Header("PlayerStats")]
    public int evolutionPoints;
    public int health;
    public float stamina;
    public int damage;
    public float swimSpeed;
    public float rotateSpeed;
    public int dashCharges;
    public int dashSpeed;
    public int depthLimit;

    [Header("Decrease")]
    public float staminaDecrease = 1.0f;
    public float dashSpeedDecrease = 1.0f;
    public float attackTimeDecrease = 5.0f;
    float currentDashSpeed = 1;

    [Header("Scripts")]
    public PlayerAttackRadius playerAttackRadius;
    public AudioManager audioManager;

    [Header("GameObjects")]
    public GameObject attackRadius;
    public Camera camera;

    // Run Results
    [HideInInspector] public int tallyEvoPoints;
    [HideInInspector] public int tallyKills;
    [HideInInspector] public int tallyFoodEaten;
    [HideInInspector] public string tallyBiome;
    [HideInInspector] public string causeOfDeath;

    // Find Scripts

    // Inputs 
    float horizontalMove;
    float verticalMove;
    bool attackButton;
    bool dashButton;


    Vector3 theScale;
    Color spriteColor;

    //Timing
    float currentAttackTime = 0;
    float attackTime = 0.2f;
    float attackLength = 0.05f;
    bool attacking = false;

    float startFrameTime = 2;
    public float frameTime;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        ResetRunStats();
    }

    private void FixedUpdate()
    {
        InputMananger();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        theScale = transform.localScale;

        InvincibilityFrames();
        CheckState();
        AttackManager();
        FlipCharacterModel();
        DrainManager();
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
        attackButton = Input.GetButton("Attack");

        //Dash Input
        dashButton = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1);
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.right * verticalMove * swimSpeed * currentDashSpeed * Time.deltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -horizontalMove * rotateSpeed * Time.deltaTime));

        if (dashButton && dashCharges > 0)
        {
            currentDashSpeed = dashSpeed;
            dashCharges -= 1;
            audioManager.PlayPlayerDash();
        }

        //Play Audio
        if (verticalMove > 0 || verticalMove < 0)
        {
            //audioManager.PlayPlayerSwim();
        }
    }

    private void AttackManager()
    {
        TakeDamage takeDamage = playerAttackRadius.takeDamage;
        FoodCharacter food = playerAttackRadius.foodScript;
        // Attack Input
        if (Attacking() == true)
        {
            attackRadius.SetActive(true);
        }
        else attackRadius.SetActive(false);

        // Attack Target        
        if (playerAttackRadius.attackCurrentFish) takeDamage.health -= damage;

        // Consume Food       
        if (playerAttackRadius.eatCurrentFood)
        {
            evolutionPoints += food.evolutionPoints;
            stamina += food.staminaPoints;
            if (playerAttackRadius.eatCurrentFood) takeDamage.health -= damage;

        }
    }

    public void InvincibilityFrames()
    {
        if (hit && isDead == false)
        {
            frameTime -= Time.deltaTime;
            takeDamage.canTakeDamage = false;
            spriteColor.a = 0.2f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
            if (frameTime <= 0)
            {
                frameTime = 0;
                hit = false;
            }
        }

        else if (isDead)
        {
            hit = false;
        }

        else
        {
            frameTime = startFrameTime;
            takeDamage.canTakeDamage = true;
            spriteColor.a = 1f;
            spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = spriteColor;
        }
    }

    bool Attacking()
    {
        StopAttacking();
        if (attackButton && currentAttackTime == 0)
        {
            audioManager.PlayPlayerReadyingBite();
            currentAttackTime = attackTime;
            attacking = true;
            return false;
        }

        else if (attacking == true && currentAttackTime <= attackLength)
        {
            audioManager.PlayPlayerBite();
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

    void DashDrain()
    {
        currentDashSpeed -= dashSpeedDecrease * Time.deltaTime;

        // Clamp
        if (currentDashSpeed <= 1)
        {
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

    void DrainManager()
    {
        StaminaDrain();
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
        depthLimit = baseDepthLimit;

        tallyEvoPoints = 0;
        tallyFoodEaten = 0;
        tallyKills = 0;
        tallyBiome = "Shallow";
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
        baseDepthLimit = 0;

        health = baseHealth;
        takeDamage.health = health;
        stamina = baseStamina;
        damage = baseDamage;
        swimSpeed = baseSwimSpeed;
        rotateSpeed = baseRotateSpeed;
        dashCharges = baseDashCharges;
        dashSpeed = baseDashSpeed;
        depthLimit = baseDepthLimit;
    }

    public new void CheckState()
    {
        if (health <= 0 || takeDamage.health <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
