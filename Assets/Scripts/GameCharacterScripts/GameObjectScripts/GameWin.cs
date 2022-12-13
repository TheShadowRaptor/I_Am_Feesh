using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    public GameObject gameManagerObj;
    public GameManager gameManager;

    private void Update()
    {
        if (gameManagerObj == null)
        {
            gameManagerObj = GameObject.Find("GameManager");
            gameManager = gameManagerObj.GetComponent<GameManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && gameManager.state == GameManager.GameState.gameplay) 
        {
            gameManager.WinGame();
        }
    }
}
