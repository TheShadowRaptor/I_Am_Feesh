using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerStats")]
    public int health = 1;
    public int playerDamage = 1;
    public float swimSpeed = 50;

    [Header("Scripts")]
    public PlayerAttackRadius playerAttackRadius;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackTarget();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalMove, verticalMove) * swimSpeed * Time.deltaTime;
    }

    private void AttackTarget()
    {
        EnemyHealth enemyHealthScript = playerAttackRadius.enemyHealthScript;
        if(playerAttackRadius.attacking) enemyHealthScript.health = health - playerDamage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

        }
    }
}
