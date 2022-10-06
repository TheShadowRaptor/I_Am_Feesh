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

    public int playerDamage = 1;
    public float swimSpeed = 50;
    public float rotateSpeed = 10;

    [Header("Scripts")]
    public PlayerAttackRadius playerAttackRadius;

    [Header("GameObjects")]
    public GameObject attackRadius;

    public float playerStamina;

    // Inputs 
    float horizontalMove;
    float verticalMove;

    bool attackButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = startStamina;
    }

    // Update is called once per frame
    void Update()
    {
        AttackTarget();
        FlipCharacterModel();
        StaminaDrain();
        CheckState();
        Lose();
    }

    private void FixedUpdate()
    {
        InputMananger();
        Move();
    }

    protected new void FlipCharacterModel()
    {
        float moveDir = transform.rotation.eulerAngles.z;

        if (moveDir > 90 && moveDir < 180 && c_FacingRight)
        {
            c_FacingRight = !c_FacingRight;

            Vector3 theScale = transform.localScale;
            theScale.y *= -1;
            transform.localScale = theScale;
        }

        if (moveDir > 0 && moveDir < 90 && !c_FacingRight)
        {
            c_FacingRight = !c_FacingRight;

            Vector3 theScale = transform.localScale;
            theScale.y *= -1;
            transform.localScale = theScale;
        }
    }

    protected void Lose()
    {
        if (isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
