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
        if (other.gameObject.CompareTag("Player")) 
        {
            gameManager.WinGame();
        }
    }
    //void OnGUI()
    //{
    //    GUIStyle header = new GUIStyle();
    //    GUIStyle normal = new GUIStyle();
    //    GUI.color = Color.cyan;
    //    header.fontSize = 20;
    //    normal.fontSize = 12;

    //    float x = 10;
    //    float y = 5;
    //    float width = 200;
    //    float height = 20;

    //    float margin = 1;
    //    float yLocation = y;


    //    GUI.Label(new Rect(x, yLocation, width, height), "DEBUGCONSOLE", header);
    //    yLocation += height + margin;
    //    GUI.Label(new Rect(x, yLocation, width, height), "FPS" + 1.0f / Time.deltaTime, normal);
    //    yLocation += height + margin;
    //    GUI.Label(new Rect(x, yLocation, width, height), "GameManagerObj: " + gameManagerObj, normal);
    //    yLocation += height + margin;
    //    GUI.Label(new Rect(x, yLocation, width, height), "GameManager: " + gameManager, normal);
    //}

}
