using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float swimSpeed = 50;
    Rigidbody2D rb;
    private float horizontalMove;
    private float verticalMove;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontalMove, verticalMove) * swimSpeed * Time.deltaTime;
    }
}
