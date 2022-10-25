using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GameCharacter
{
    [Header("PlayerStats")]

    public int evolutionPoints;
    public int baseHealth = 1;
    public float baseStamina;
    public int baseDamage = 1;
    public int baseDashCharges = 1;
    public int baseDashSpeed = 2;
    public float baseSwimSpeed = 50;
    public float baseRotateSpeed = 10;

    [Header("Decrease")]
    public float staminaDecrease = 1.0f;
    public float dashSpeedDecrease = 1.0f;
    public float attackTimeDecrease = 5.0f;

    [Header("Scripts")]
    public PlayerAttackRadius playerAttackRadius;

    [Header("GameObjects")]
    public GameObject attackRadius;
    public Camera camera;

    [HideInInspector] public int health;
    [HideInInspector] public float stamina;
    [HideInInspector] public int damage;
    [HideInInspector] public float swimSpeed;
    [HideInInspector] public float rotateSpeed;
    [HideInInspector] public int dashCharges;
    [HideInInspector] public int dashSpeed;

    // Find Scripts

    // Inputs 
    float horizontalMove;
    float verticalMove;
    bool attackButton;
    bool dashButtonDown;

    Vector3 theScale; 

    //Attack Timing
    public float currentAttackTime = 0;
    float currentDashSpeed = 1;
    float attackTime = 0.2f;
    float attackLength = 0.1f;

    bool attacking = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetStats();
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
        dashButtonDown = Input.GetButtonDown("Dash");
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.right * verticalMove * swimSpeed * currentDashSpeed * Time.deltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -horizontalMove * rotateSpeed * Time.deltaTime));

        if (dashButtonDown && dashCharges > 0)
        {
            currentDashSpeed = dashSpeed;
            dashCharges -= 1;
        }
    }

    private void AttackManager()
    {
        TakeDamage takeDamage = playerAttackRadius.takeDamage;
        FoodCharacter food = playerAttackRadius.foodScript;
        // Attack Input
        if (Attacking() == true) attackRadius.SetActive(true);
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

    bool Attacking()
    {
        StopAttacking();
        if (attackButton && currentAttackTime == 0)
        {
            currentAttackTime = attackTime;
            attacking = true;
            return false;
        }

        else if (attacking == true && currentAttackTime <= attackLength)
        {
            return true;
        }

        else
        {

            return false;
        }
    }

    void StopAttacking()
    {
        if (currentAttackTime <= 0)
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

    public void ResetStats()
    {
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
