using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : FishCharacter
{    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {      
        FlipCharacter();
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
    }      
}
