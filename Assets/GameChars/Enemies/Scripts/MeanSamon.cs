using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanSamon : FishCharacter
{
    public EnemyAttackRadius attackRadius;
    public GameObject attackRadiusObj;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        renderer = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        damage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        FlipCharacterModel();
        CheckState();
        SpawnFood();
        Deactivate();
    }

    void FixedUpdate()
    {
        if (renderer.isVisible)
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
        }

        else
        {
            if (changeDir == false) transform.rotation = new Quaternion(0, 0, 0, 0);
            else if (changeDir == true) transform.rotation = new Quaternion(0, 0, 180, 0);
        }
    }

    protected new void SpawnFood()
    {
        if (isDead)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(food, transform.position, transform.rotation);
                Debug.Log("Dead");
            }
        }
    }

    private void AttackManager()
    {
        TakeDamage takeDamage = attackRadius.takeDamage;
        // Attack Input
        if (Attacking()) attackRadiusObj.SetActive(true);
        else attackRadiusObj.SetActive(false);

        // Attack Target        
        if (attackRadius.attackPlayer) takeDamage.health -= damage;
    }

    bool Attacking()
    {
        if (attackRadiusObj.activeSelf)
        {
            currentAttackTime = attackTime;
            return false;
        }
        else if (currentAttackTime > 0) return true;
        else return false;
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
