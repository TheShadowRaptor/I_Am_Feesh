using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMananger : MonoBehaviour
{
    public GameObject level;
    // Used to save currentSceneNumber for export
    public int currentSceneNum;
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
