using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : MonoBehaviour
{
    [Header("SmallFishStats")]    
    public float swimSpeed = 20;
    public float fleeSwimSpeed = 40;

    [Header("Scripts")]
    public FishDetection DetectionRadius;

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
        AnimationHandler();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Move
        rb.velocity = transform.right * swimSpeed * Time.deltaTime;
        if (PlayerSpotted())
        {
            // Swim Sway
            transform.right = -player.transform.position + transform.position;
            swimSpeed = fleeSwimSpeed;
        }
    }

    private void AnimationHandler()
    {
        float moveDir = rb.velocity.x;

        if (moveDir > 0 && !p_FacingRight || moveDir < 0 && p_FacingRight) Flip();
    }

    void Flip()
    {
        p_FacingRight = !p_FacingRight;

        Quaternion theRotation = transform.localRotation;
        theRotation.y *= -1;
        transform.localRotation = theRotation;
    }

    public bool PlayerSpotted()
    {
        if (DetectionRadius.SpottedPlayer)
        {           
            return true;
        }
        return false;
    }
}
