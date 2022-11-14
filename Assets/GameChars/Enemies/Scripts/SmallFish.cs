using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFish : FishCharacter
{
    public bool hit = false;
    public Renderer spriteRenderer;
    // Components
    Color spriteColor;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = spriteRenderer.GetComponent<SpriteRenderer>();
        spriteColor = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        player = GameObject.Find("Player");
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
        if (spriteRenderer.isVisible)
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
