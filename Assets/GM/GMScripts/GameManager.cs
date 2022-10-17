using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        title,
        controls,
        settings,
        gameplay,
        pause,
        results,
        upgrade,
        win
    }

    public UIMananger uIMananger;
    public LevelMananger levelMananger;
    GameObject playerObj;
    PlayerController player;

    GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.title;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }

        if (player.isDead)
        {
            state = GameState.results;
            player.ResetStats();
        }

        switch (state)
        {
            case GameState.title:
                Time.timeScale = 0;
                uIMananger.TitleCanvasOn();
                break;

            case GameState.controls:
                Time.timeScale = 0;
                uIMananger.ControlsCanvasOn();
                break;

            case GameState.settings:
                Time.timeScale = 0;
                uIMananger.SettingsCanvasOn();
                break;

            case GameState.gameplay:
                Time.timeScale = 1;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = GameState.pause;
                }
                uIMananger.GameplayCanvasOn();
                break;

            case GameState.pause:
                Time.timeScale = 0;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = GameState.gameplay;
                }
                uIMananger.PauseCanvasOn();
                break;

            case GameState.results:
                Time.timeScale = 0;
                uIMananger.ResultsCanvasOn();
                break;

            case GameState.upgrade:
                Time.timeScale = 0;
                uIMananger.UpgradeCanvasOn();
                break;

            case GameState.win:
                Time.timeScale = 0;
                uIMananger.WinCanvasOn();
                break;
        }
    }

    public void LoadLevelButton()
    {
        levelMananger.LoadScene(1);
        state = GameState.gameplay;
    }

    public void UnPauseButton()
    {
        state = GameState.gameplay;
    }

    public void LoadTitleButton()
    {
        levelMananger.LoadScene(0);
        state = GameState.title;
    }

    public void LoadSettingsButton()
    {
        state = GameState.settings;
    }

    public void BackButton()
    {
        if (levelMananger.currentSceneNum == 0) state = GameState.title;
        else state = GameState.pause;
    }

    public void LoadUpgradeButton()
    {
        state = GameState.upgrade;
    }

    public void LoadControlsButton()
    {
        state = GameState.controls;
    }
}
