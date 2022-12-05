using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        if (playerPos.y > 0)
        {
            playerPos.y = 0;
        }

        if (playerPos.x > 5)
        {
            playerPos.x = 5;
        }

        if (playerPos.x < -5)
        {
            playerPos.x = -5;
        }

        this.transform.position = playerPos;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
