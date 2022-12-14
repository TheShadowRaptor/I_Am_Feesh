using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource playerAudio;
    public AudioSource playerBiteAudio;
    public AudioSource objectAudio;
    public AudioSource enemyAudio;
    public AudioSource uIAudio;
    public AudioSource musicAudio;
    public AudioSource battleMusicAudio;

    [Header("PlayerSounds")]
    public AudioClip playerBite;
    public AudioClip playerReadyingBite;
    public AudioClip playerDeath;
    public AudioClip playerSwim;
    public AudioClip playerDash;

    [Header("ObjectSounds")]
    public AudioClip foodEaten;

    [Header("EnemySounds")]
    public AudioClip enemyBite;

    [Header("UISounds")]
    public AudioClip selectButton;

    [Header("Music")]
    public AudioClip gameplayMusicShallow;
    public AudioClip gameplayMusicCoralReef;
    public AudioClip gameplayMusicOpenOcean;
    public AudioClip gameplayMusicTwilight;
    public AudioClip menuMusic;

    [Header("Music")]
    public AudioClip battleMusicShallow;
    public AudioClip battleMusicCoralReef;
    public AudioClip battleMusicOpenOcean;
    public AudioClip battleMusicTwilight;

    [Header("Scripts")]
    public GameManager gameManager;
    public SettingsManager settingsManager;

    float maxMusicVolume;
    float pauseMusicVolume;
    float muteMusicVolume;

    bool inBattle;

    private void Start()
    {
        playerAudio = gameObject.transform.Find("PlayerAudio").gameObject.GetComponent<AudioSource>();
        playerBiteAudio = gameObject.transform.Find("PlayerBiteAudio").gameObject.GetComponent<AudioSource>();
        objectAudio = gameObject.transform.Find("ObjectAudio").gameObject.GetComponent<AudioSource>();
        enemyAudio = gameObject.transform.Find("EnemyAudio").gameObject.GetComponent<AudioSource>();
        uIAudio = gameObject.transform.Find("UiAudio").gameObject.GetComponent<AudioSource>();
        musicAudio = gameObject.transform.Find("MusicAudio").gameObject.GetComponent<AudioSource>();
        battleMusicAudio = gameObject.transform.Find("BattleMusicAudio").gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameManager.state == GameManager.GameState.settings)
        {
            musicAudio.volume = settingsManager.musicVolume;
            battleMusicAudio.volume = settingsManager.musicVolume;

            if (inBattle)
            {
                musicAudio.volume = muteMusicVolume;
            }
            else if (inBattle == false)
            {
                battleMusicAudio.volume = muteMusicVolume;
            }

            playerAudio.volume = settingsManager.soundVolume;
            playerBiteAudio.volume = settingsManager.soundVolume;
            objectAudio.volume = settingsManager.soundVolume;
            enemyAudio.volume = settingsManager.soundVolume;
            uIAudio.volume = settingsManager.soundVolume;
        }

        maxMusicVolume = settingsManager.musicVolume;
        pauseMusicVolume = maxMusicVolume / 3.0f;
        muteMusicVolume = 0;
    }

    //Player Sounds
    public void PlayPlayerBite()
    {
        playerBiteAudio.clip = playerBite;
        if (playerBiteAudio.isPlaying == false)
        {
            playerBiteAudio.PlayOneShot(playerBite);
        }
    }

    public void PlayPlayerReadyingBite()
    {
        playerAudio.clip = playerReadyingBite;
        playerAudio.Play();
    }

    public void PlayPlayerDeath()
    {
        playerAudio.clip = playerDeath;
        playerAudio.Play();
    }

    public void PlayPlayerSwim()
    {
        playerAudio.clip = playerSwim;
        if (playerAudio.isPlaying == false || playerBiteAudio.isPlaying == false)
        {
            playerAudio.PlayOneShot(playerSwim);
        }
    }

    public void PlayPlayerDash()
    {
        playerAudio.clip = playerDash;
        playerAudio.Play();
    }
    
    //------------------------------------------

    //Object Sounds
    public void PlayFoodEaten()
    {
        objectAudio.clip = foodEaten;
        playerAudio.PlayOneShot(foodEaten);
    }

    //Enemy Sounds
    public void PlayEnemyBite()
    {
        enemyAudio.clip = enemyBite;
        if (enemyAudio.isPlaying == false)
        {
            enemyAudio.PlayOneShot(enemyBite);
        }    
    }

    //UI Sounds
    public void PlaySelectButton()
    {
        uIAudio.clip = selectButton;
        if (uIAudio.isPlaying == false)
        {
            uIAudio.Play();
        }
    }

    //Music 
    public void PlayGameplayMusicShallow()
    {
        musicAudio.clip = gameplayMusicShallow;
        musicAudio.Play();
        musicAudio.volume = maxMusicVolume;
    }

    public void PlayGameplayMusicCoralReef()
    {
        musicAudio.clip = gameplayMusicCoralReef;
        musicAudio.Play();
        musicAudio.volume = maxMusicVolume;
    }

    public void PlayGameplayMusicOpenOcean()
    {
        musicAudio.clip = gameplayMusicOpenOcean;
        musicAudio.Play();
        musicAudio.volume = maxMusicVolume;
    }

    public void PlayGameplayMusicTwilight()
    {
        musicAudio.clip = gameplayMusicTwilight;
        musicAudio.Play();
        musicAudio.volume = maxMusicVolume;
    }

    // Battle Music

    public void PlayBattleMusicShallow()
    {
        battleMusicAudio.clip = battleMusicShallow;
        battleMusicAudio.Play();
        battleMusicAudio.volume = muteMusicVolume;
    }

    public void PlayBattleMusicCoralReef()
    {
        battleMusicAudio.clip = battleMusicCoralReef;
        battleMusicAudio.Play();
        battleMusicAudio.volume = muteMusicVolume;
    }

    public void PlayBattleMusicOpenOcean()
    {
        battleMusicAudio.clip = battleMusicOpenOcean;
        battleMusicAudio.Play();
        battleMusicAudio.volume = muteMusicVolume;
    }

    public void PlayBattleMusicTwilight()
    {
        battleMusicAudio.clip = battleMusicTwilight;
        battleMusicAudio.Play();
        battleMusicAudio.volume = muteMusicVolume;
    }

    public void PlayMenuMusic()
    {
        musicAudio.clip = menuMusic;
        if (musicAudio.isPlaying == false)
        {
            musicAudio.Play();
        }
        musicAudio.volume = maxMusicVolume;
    }

    // Misc
    public void TurnGameplayMusicDown()
    {
        musicAudio.volume = pauseMusicVolume;
    }
    
    public void TurnGameplayMusicUp()
    {
        musicAudio.volume = maxMusicVolume;
    }

    public void DynamicGameplayMusicSwitch(bool playBattleMusic)
    {
        if (playBattleMusic)
        {
            battleMusicAudio.volume = maxMusicVolume;
            musicAudio.volume = muteMusicVolume;
            inBattle = true;
        }

        else if (playBattleMusic == false)
        {
            battleMusicAudio.volume = muteMusicVolume;
            musicAudio.volume = maxMusicVolume;
            inBattle = false;
        }
    }

    public void StopGameplayMusic()
    {
        musicAudio.Stop();
        battleMusicAudio.Stop();
    }
}
