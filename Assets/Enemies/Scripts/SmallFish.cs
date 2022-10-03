using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : FishCharacter
{
    bool changeDir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {      
        FlipCharacterModel();
    }

    public bool WarningSpotted()
    {
        // Warning Causes surrounding small fish in warning radius to swim away
        if (warningBehaviour.spottedWarning)
        {
            return true;
        }
        return false;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Move
        rb.velocity = transform.right * swimSpeed * Time.deltaTime;

        if (PlayerSpotted() || WarningSpotted())
        {
            // Swim Away 
            transform.right = -player.transform.position + transform.position;
            swimSpeed = fleeSwimSpeed;
        }

        else
        {
            if (changeDir == false) transform.rotation = new Quaternion(0, 0, 0, 0);
            else if (changeDir == true) transform.rotation = new Quaternion(0, 0, 180, 0);
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
