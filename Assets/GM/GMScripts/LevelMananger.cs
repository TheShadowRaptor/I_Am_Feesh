using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMananger : MonoBehaviour
{
    public GameObject level;
    // Used to save currentSceneNumber for export
    public int currentSceneNum;

    private void Update()
    {
        if (level == null)
        {
            level = GameObject.Find("LevelOne");
        }
    }
    public void EndGame()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
        level.SetActive(true);
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(0);
    }
}
