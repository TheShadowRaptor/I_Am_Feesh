using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    [Header("SmallFishStats")]    
    public float swimSpeed = 20;
    public float fleeSwimSpeed = 40;

    [Header("Scripts")]
    public DetectPlayer detectPlayer;
    public DetectWarning warningBehaviour;

    Rigidbody2D rb;
    GameObject player;

    private bool p_FacingRight = true;

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

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Move
        rb.velocity = transform.right * swimSpeed * Time.deltaTime;


        if (PlayerSpotted()|| WarningSpotted())
        {
            // Swim Away 
            transform.right = -player.transform.position + transform.position;
            swimSpeed = fleeSwimSpeed;
        }
    }

    private void FlipCharacter()
    {
        float moveDir = rb.velocity.x;

        if (moveDir > 0 && !p_FacingRight || moveDir < 0 && p_FacingRight) Flip();
    }

    void Flip()
    {
        p_FacingRight = !p_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }

    public bool PlayerSpotted()
    {
        if (detectPlayer.spottedPlayer)
        {           
            return true;
        }
        return false;
    }

    public bool WarningSpotted()
    {
        if (warningBehaviour.spottedWarning)
        {
            return true;
        }
        return false;
    }
}
