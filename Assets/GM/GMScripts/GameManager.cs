using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using System.IO;

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
    GameObject playerSpawn;

    public GameObject warningPanel;
    public TextMeshProUGUI SaveDataDoesNotExist;

    PlayerController player;

    //Text timer
    float timeStart = 3;
    float timeLeft = 3;

    GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.title;        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }

        if (playerSpawn == null)
        {
            playerSpawn = GameObject.Find("PlayerSpawn");
        }

        if (player.isDead)
        {
            state = GameState.results;
            player.ResetStats();
        }

        //Text Timer
        if (SaveDataDoesNotExist.enabled == true)
        {
            // Timescale is 0 so I use 0.01 instead to subtract time
            timeLeft -= 0.01f;
            if (timeLeft <= 0)
            {
                timeLeft = timeStart;
                SaveDataDoesNotExist.enabled = false;
            }
        }

        switch (state)
        {
            case GameState.title:
                Time.timeScale = 0;
                player.transform.position = playerSpawn.transform.position;
                player.transform.rotation = playerSpawn.transform.rotation;
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
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = GameState.gameplay;
                }
                uIMananger.PauseCanvasOn();
                Time.timeScale = 0;
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
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
        Save();
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
    public void LoadUpgradeButton()
    {
        Save();
        state = GameState.upgrade;
    }

    public void LoadControlsButton()
    {
        state = GameState.controls;
    }

    public void LoadSaveButton()
    {
        if (File.Exists(Application.persistentDataPath + "/savefile1.dat"))
        {
            Load();
            LoadLevelButton();
        }
        else
        {
            SaveDataDoesNotExist.enabled = true;
        }
    }

    public void UnPauseButton()
    {
        state = GameState.gameplay;
    }

    public void BackButton()
    {
        if (levelMananger.currentSceneNum == 0) state = GameState.title;
        else state = GameState.pause;
    }

    public void showWarningPanel()
    {
        warningPanel.SetActive(true);
    }

    public void hideWarningPanel()
    {
        warningPanel.SetActive(false);
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savefile1.dat");

        SaveData saveData = new SaveData();

        saveData.evolutionPoints = player.evolutionPoints;
        saveData.startHealth = player.startHealth;
        saveData.startStamina = player.startStamina;
        saveData.startDashSpeed = player.startDashSpeed;
        saveData.startDashCharges = player.startDashCharges;
        saveData.startSwimSpeed = player.startSwimSpeed;
        saveData.startRotateSpeed = player.startRotateSpeed;

        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/savefile1.dat", FileMode.Open);

        SaveData saveData = (SaveData)bf.Deserialize(file);
        file.Close();

        player.evolutionPoints = saveData.evolutionPoints;
        player.startHealth = saveData.startHealth;
        player.startStamina = saveData.startStamina;
        player.startDashSpeed = saveData.startDashSpeed;
        player.startDashCharges = saveData.startDashCharges;
        player.startSwimSpeed = saveData.startSwimSpeed;
        player.startRotateSpeed = saveData.startRotateSpeed;
    }

    public void OnGUI()
    {

    }
}
