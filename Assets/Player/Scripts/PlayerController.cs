using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerStats")]
    public int playerEvolutionPoints;
    public float startStamina;
    public float staminaDecrease = 1.0f;

    public int playerDamage = 1;
    public float swimSpeed = 50;

    [Header("Scripts")]
    public PlayerAttackRadius playerAttackRadius;

    [Header("GameObjects")]
    public GameObject attackRadius;

    Rigidbody2D rb;
    private bool c_FacingRight = true;
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
        FlipCharacter();
        StaminaDrain();
    }

    private void FixedUpdate()
    {
        InputMananger();
        Move();
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
        rb.velocity = new Vector2(horizontalMove, verticalMove) * swimSpeed * Time.deltaTime;
    }

    private void AttackTarget()
    {
        characterHealth characterHealth = playerAttackRadius.characterHealth;
        FoodCharacter food = playerAttackRadius.foodScript;
        // Attack Input
        if (attackButtonDown) attackRadius.SetActive(true);
        else attackRadius.SetActive(false);

        // Attack Target        
        if (playerAttackRadius.attackCurrentFish) characterHealth.health -= playerDamage;

        // Consume Food       
        if (playerAttackRadius.eatCurrentFood)
        {
            playerEvolutionPoints += food.evolutionPoints;
            playerStamina += food.staminaPoints;
            if (playerAttackRadius.eatCurrentFood) characterHealth.health -= playerDamage;

        }
    }

    private void FlipCharacter()
    {
        float moveDir = rb.velocity.x;

        if (moveDir > 0 && !c_FacingRight || moveDir < 0 && c_FacingRight) Flip();
    }

    void Flip()
    {
        c_FacingRight = !c_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void StaminaDrain()
    {
        playerStamina -= staminaDecrease * Time.deltaTime;

        // Clamp
        if (playerStamina <= 0)
        {
            playerStamina = 0;
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
