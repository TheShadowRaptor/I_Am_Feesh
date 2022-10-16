using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : GameCharacter
{
    [Header("PlayerStats")]
    public int playerEvolutionPoints;
    public float startStamina;
    public float staminaDecrease = 1.0f;

    public int startDamage = 1;
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
    

    // Inputs 
    float horizontalMove;
    float verticalMove;

    Vector3 theScale; 
    bool attackButtonDown;

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
        AttackTarget();
        FlipCharacterModel();
        StaminaDrain();
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
        attackButtonDown = Input.GetButton("Attack");
    }

    private void Move()
    {
        rb.velocity = (Vector2)transform.right * verticalMove * swimSpeed * Time.deltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -horizontalMove * rotateSpeed * Time.deltaTime));
    }

    private void AttackTarget()
    {
        TakeDamage takeDamage = playerAttackRadius.takeDamage;
        FoodCharacter food = playerAttackRadius.foodScript;
        // Attack Input
        if (attackButtonDown) attackRadius.SetActive(true);
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

    public void ResetStats()
    {
        playerStamina = startStamina;
        playerDamage = startDamage;
        swimSpeed = startSwimSpeed;
        rotateSpeed = startRotateSpeed;
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
