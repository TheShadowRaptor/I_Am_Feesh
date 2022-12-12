using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    public GameObject player;
    public GameManager gameManager;
    public bool isEnabled = false;

    private GameObject playerSpawnObj;

    public float lerpTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpawnObj == null)
        {
            playerSpawnObj = GameObject.Find("PlayerSpawn");
        }

        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        Vector3 playerSpawnPos = new Vector3(playerSpawnObj.transform.position.x, player.transform.position.y, -10);

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

        if (isEnabled == true)
        {
            Vector3 camMove = Vector3.Lerp(gameObject.transform.position, playerPos, lerpTime * Time.deltaTime);
            this.transform.position = camMove;
        }
        else if (gameManager.state == GameManager.GameState.title)
        {
            this.transform.position = playerSpawnPos;
        }

        else if (gameManager.state == GameManager.GameState.upgrade)
        {
            this.transform.position = playerSpawnPos;
        }
    }
}
