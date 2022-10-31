using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    GameObject gameManagerObj;

    private void Start()
    {
        gameManagerObj = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManagerObj.GetComponent<GameManager>().WinGame();
        }
    }
}
