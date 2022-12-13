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
    public AudioClip gameplayMusic;
    public AudioClip menuMusic;

    [Header("Scripts")]
    public SettingsManager settingsManager;

    float maxMusicVolume;
    float pauseMusicVolume;

    private void Start()
    {
        playerAudio = gameObject.transform.Find("PlayerAudio").gameObject.GetComponent<AudioSource>();
        playerBiteAudio = gameObject.transform.Find("PlayerBiteAudio").gameObject.GetComponent<AudioSource>();
        objectAudio = gameObject.transform.Find("ObjectAudio").gameObject.GetComponent<AudioSource>();
        enemyAudio = gameObject.transform.Find("EnemyAudio").gameObject.GetComponent<AudioSource>();
        uIAudio = gameObject.transform.Find("UIAudio").gameObject.GetComponent<AudioSource>();
        musicAudio = gameObject.transform.Find("MusicAudio").gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        musicAudio.volume = settingsManager.musicVolume;
        playerAudio.volume = settingsManager.soundVolume;
        playerBiteAudio.volume = settingsManager.soundVolume;
        objectAudio.volume = settingsManager.soundVolume;
        enemyAudio.volume = settingsManager.soundVolume;
        uIAudio.volume = settingsManager.soundVolume;

        maxMusicVolume = settingsManager.musicVolume;
        pauseMusicVolume = maxMusicVolume / 3.0f;
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
    public void PlayGameplayMusic()
    {
        musicAudio.clip = gameplayMusic;
        musicAudio.Play();
        musicAudio.volume = maxMusicVolume;
    }

    public void PlayMenuMusic()
    {
        musicAudio.clip = menuMusic;
        if (musicAudio.isPlaying == false)
        {
            musicAudio.Play();
        }
    }

    public void TurnGameplayMusicDown()
    {
        musicAudio.volume = pauseMusicVolume;
    }
    
    public void TurnGameplayMusicUp()
    {
        musicAudio.volume = maxMusicVolume;
    }

    public void StopGameplayMusic()
    {
        musicAudio.Stop();
    }
}
