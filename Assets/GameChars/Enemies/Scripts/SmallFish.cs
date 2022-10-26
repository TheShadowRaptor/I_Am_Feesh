using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : FishCharacter
{
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {       
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        renderer = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioManager == null)
        {
            audioManager = GameObject.Find("AudioManager").gameObject.GetComponent<AudioManager>();
        }
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
