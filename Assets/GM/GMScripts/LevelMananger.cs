using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMananger : MonoBehaviour
{
    // Used to save currentSceneNumber for export
    public int currentSceneNum;
    private void Update()
    {
        EndGame();
    }

    public void EndGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }

    public void LoadScene(int num)
    {
        currentSceneNum = num;
        SceneManager.LoadScene(num);
    }
}
