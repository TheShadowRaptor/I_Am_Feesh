using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GameCharacter
{
    [Header("PlayerStats")]
    public int playerEvolutionPoints;
    public float staminaDecrease = 1.0f;
    public float dashSpeedDecrease = 1.0f;
    public float attackTimeDecrease = 1.0f;

    public float startStamina;
    public int startDamage = 1;
    public int startDashCharges = 1;
    public int startDashSpeed = 2;
    public float startSwimSpeed = 50;
    public float startRotateSpeed = 10;

    [Header("Scripts")]
    public GameManager gameManager;
    public PlayerAttackRadius playerAttackRadius;

    [Header("GameObjects")]
    public GameObject attackRadius;

    [HideInInspector] public float playerStamina;
    [HideInInspector] public int playerDamage;
    [HideInInspector] public float swimSpeed;
    [HideInInspector] public float rotateSpeed;
    [HideInInspector] public int dashCharges;
    [HideInInspector] public int playerDashSpeed;


    // Inputs 
    float horizontalMove;
    float verticalMove;
    bool attackButton;
    bool dashButtonDown;

    Vector3 theScale; 
    float currentDashSpeed = 1;
    float currentAttackTime = 0;
    float attackTime = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetStats();
    }

    // Update is called once per frame
    void Update()
    {
        theScale = transform.localScale;
        AttackManager();
        FlipCharacterModel();
        DrainManager();
        CheckState();
    }

    private void FixedUpdate()
    {
        InputMananger();
        Move();
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
            currentDashSpeed = playerDashSpeed;
            dashCharges -= 1;
        }
    }

    private void AttackManager()
    {
        TakeDamage takeDamage = playerAttackRadius.takeDamage;
        FoodCharacter food = playerAttackRadius.foodScript;
        // Attack Input
        if (Attacking()) attackRadius.SetActive(true);
        else attackRadius.SetActive(false);

        // Attack Target        
        if (playerAttackRadius.attackCurrentFish) takeDamage.health -= playerDamage;

        // Consume Food       
        if (playerAttackRadius.eatCurrentFood)
        {
            playerEvolutionPoints += food.evolutionPoints;
            playerStamina += food.staminaPoints;
            if (playerAttackRadius.eatCurrentFood) takeDamage.health -= playerDamage;

        }
    }

    bool Attacking()
    {
        if (attackButton && currentAttackTime == 0)
        {
            currentAttackTime = attackTime;
            return true;
        }
        else if (currentAttackTime > 0) return true;
        else return false;
    }

    void StaminaDrain()
    {
        playerStamina -= staminaDecrease * Time.deltaTime;

        // Clamp
        if (playerStamina <= 0)
        {
            playerStamina = 0;
            isDead = true;
        }

        if (playerStamina > startStamina)
        {
            playerStamina = startStamina;
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

        if (currentDashSpeed > playerDashSpeed)
        {
            currentDashSpeed = playerDashSpeed;
        }
    }

    void AttackTimeDrain()
    {
        currentAttackTime -= attackTimeDecrease;

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
        playerStamina = startStamina;
        playerDamage = startDamage;
        swimSpeed = startSwimSpeed;
        rotateSpeed = startRotateSpeed;
        dashCharges = startDashCharges;
        playerDashSpeed = startDashSpeed;
    }

    public new void CheckState()
    {
        if (takeDamage.health <= 0)
        {
            isDead = true;
        }
        else if (playerStamina <= 0)
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
