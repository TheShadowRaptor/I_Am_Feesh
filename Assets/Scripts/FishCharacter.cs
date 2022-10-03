using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : GameCharacter
{
    [Header("FishStats")]
    public float swimSpeed = 20;
    public float fleeSwimSpeed = 40;

    [Header("Scripts")]
    public DetectPlayer detectPlayer;
    public DetectWarning warningBehaviour;

    protected Rigidbody2D rb;
    protected GameObject player;

    protected bool c_FacingRight = true;

    Vector3 theScale;
    protected void FlipCharacter()
    {
        float moveDir = rb.velocity.x;

        if (moveDir > 0 && !c_FacingRight || moveDir < 0 && c_FacingRight) Flip();
    }

    void Flip()
    {
        c_FacingRight = !c_FacingRight;

        theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }
    public bool PlayerSpotted()
    {
        // If player is spotted becomes true
        if (detectPlayer.spottedPlayer)
        {
            return true;
        }
        return false;
    }   

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerSpotted() == false)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Wall"))
            {
                transform.right = -transform.right;
                if (c_FacingRight == true || c_FacingRight == false)
                {
                    theScale = transform.localScale;
                    theScale.y *= -1;
                    transform.localScale = theScale;
                }
            }
        }        
    }
}
