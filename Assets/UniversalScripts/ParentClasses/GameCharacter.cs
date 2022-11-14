using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public TakeDamage takeDamage;
    public bool isDead = false;

    protected Rigidbody2D rb;
    protected bool c_FacingRight = true;

    [Header("InvincibilityFrames")]
    public float hitFrameTime;
    protected float startHitFrameTime = 2;

    protected void FlipCharacterModel()
    {
        float moveDir = rb.velocity.x;

        if (moveDir > 0 && !c_FacingRight || moveDir < 0 && c_FacingRight) Flip();
    }

    void Flip()
    {
        c_FacingRight = !c_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.y *= -1;
        transform.localScale = theScale;
    }

    public void CheckState()
    {
        if (takeDamage.health <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    public void Deactivate()
    {
        if (isDead == true)
        {
            gameObject.SetActive(false);
        }
    }
}
