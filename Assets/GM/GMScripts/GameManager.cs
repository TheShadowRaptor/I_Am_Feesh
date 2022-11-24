using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("GameManagers")]
    public UIMananger uIMananger;
    public LevelMananger levelMananger;
    public AudioManager audioManager;
    public UpgradeManager upgradeManager;

    [Header("TextObjects")]
    public TextMeshProUGUI SaveDataDoesNotExist;
    
    [Header("GameObjects")]
    public GameObject warningPanel;
    public GameObject SavingPanel;

    public GameObject playerObj;
    public GameObject playerSpawn;

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
        if (playerSpawn == null)
        {
            playerSpawn = GameObject.Find("PlayerSpawn");
        }

        if (player == null && playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (player.isDead)
        {
            state = GameState.results;
        }

        //Text Timer-----------------------------
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

        if (SavingPanel.activeSelf == true)
        {
            // Timescale is 0 so I use 0.01 instead to subtract time
            timeLeft -= 0.01f;
            if (timeLeft <= 0)
            {
                timeLeft = timeStart;
                SavingPanel.SetActive(false);
            }
        }
        //--------------------------------------

        switch (state)
        {
            case GameState.title:
                Time.timeScale = 0;
                //Set Player position to PlayerSpawn point
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
                audioManager.StopGameplayMusic();
                break;

            case GameState.upgrade:
                Time.timeScale = 0;
                player.ResetRunStats();
                uIMananger.UpgradeCanvasOn();
                break;

            case GameState.win:
                Time.timeScale = 0;
                uIMananger.WinCanvasOn();
                break;
        }
    }

    public void NewGame()
    {
        DeleteSave();
        player.FullyResetStats();
        LoadLevelButton();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {      
        playerSpawn = GameObject.Find("PlayerSpawn");
        //Set Player position to PlayerSpawn point
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
    }

    public void LoadLevelButton()
    {
        levelMananger.LoadLevel();

        //Set Player position to PlayerSpawn point
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;

        audioManager.PlayGameplayMusic();
        Save();

        state = GameState.gameplay;
    }
    public void LoadTitleButton()
    {
        levelMananger.LoadTitle();
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

    public void WinGame()
    {
        state = GameState.win;
    }


    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savefile1.dat");

        SaveData saveData = new SaveData();

        // PlayerData
        saveData.evolutionPoints = player.evolutionPoints;
        saveData.health = player.baseHealth;
        saveData.stamina = player.baseStamina;
        saveData.dashSpeed = player.baseDashSpeed;
        saveData.dashCharges = player.baseDashCharges;
        saveData.swimSpeed = player.baseSwimSpeed;
        saveData.rotateSpeed = player.baseRotateSpeed;
        saveData.depthLimit = player.baseDepthLimit;

        // UpgradeData
        saveData.currentSwimSpeedButtonState = upgradeManager.currentSwimSpeedButtonState;
        saveData.currentTurnSpeedButtonState = upgradeManager.currentTurnSpeedButtonState;
        saveData.currentStomachCapacityButtonState = upgradeManager.currentStomachCapacityButtonState;
        saveData.currentEvolveButtonStateCap = upgradeManager.currentEvolveButtonStateCap;

        saveData.currentSwimSpeedButtonPriceState = upgradeManager.currentSwimSpeedButtonPriceState;
        saveData.currentTurnSpeedButtonPriceState = upgradeManager.currentTurnSpeedButtonPriceState;
        saveData.currentStomachCapacityPriceButtonState = upgradeManager.currentStomachCapacityButtonPriceState;

        saveData.evolutionStage = upgradeManager.evolutionStage;

    bf.Serialize(file, saveData);
        file.Close();

        SavingPanel.SetActive(true);
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/savefile1.dat", FileMode.Open);

        SaveData saveData = (SaveData)bf.Deserialize(file);
        file.Close();

        // PlayerData
        player.evolutionPoints = saveData.evolutionPoints;
        player.baseHealth = saveData.health;
        player.baseStamina = saveData.stamina;
        player.baseDashSpeed = saveData.dashSpeed;
        player.baseDashCharges = saveData.dashCharges;
        player.baseSwimSpeed = saveData.swimSpeed;
        player.baseRotateSpeed = saveData.rotateSpeed;
        player.baseDepthLimit = saveData.depthLimit;

        // UpgradeData
        upgradeManager.currentSwimSpeedButtonState = saveData.currentSwimSpeedButtonState;
        upgradeManager.currentTurnSpeedButtonState = saveData.currentTurnSpeedButtonState;
        upgradeManager.currentStomachCapacityButtonState = saveData.currentStomachCapacityButtonState;
        upgradeManager.currentEvolveButtonStateCap = saveData.currentEvolveButtonStateCap;

        upgradeManager.currentSwimSpeedButtonPriceState = saveData.currentSwimSpeedButtonPriceState;
        upgradeManager.currentTurnSpeedButtonPriceState = saveData.currentTurnSpeedButtonPriceState;
        upgradeManager.currentStomachCapacityButtonPriceState = saveData.currentStomachCapacityPriceButtonState;

        upgradeManager.evolutionStage = saveData.evolutionStage;
    }

    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/savefile1.dat");
    }

    public void OnGUI()
    {

    }
}
