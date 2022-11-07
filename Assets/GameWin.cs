using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    GameObject gameManagerObj;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameManagerObj == null)
        {
            gameManagerObj = GameObject.Find("GameManager");
        }

        if (other.gameObject.CompareTag("Player")) 
        {
            gameManagerObj.GetComponent<GameManager>().WinGame();
        }
    }
}
