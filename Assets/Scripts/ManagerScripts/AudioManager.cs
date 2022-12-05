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

    private void Start()
    {
        playerAudio = gameObject.transform.Find("PlayerAudio").gameObject.GetComponent<AudioSource>();
        playerBiteAudio = gameObject.transform.Find("PlayerBiteAudio").gameObject.GetComponent<AudioSource>();
        objectAudio = gameObject.transform.Find("ObjectAudio").gameObject.GetComponent<AudioSource>();
        enemyAudio = gameObject.transform.Find("EnemyAudio").gameObject.GetComponent<AudioSource>();
        uIAudio = gameObject.transform.Find("UIAudio").gameObject.GetComponent<AudioSource>();
        musicAudio = gameObject.transform.Find("MusicAudio").gameObject.GetComponent<AudioSource>();
    }

    //Player Sounds
    public void PlayPlayerBite()
    {
        playerBiteAudio.clip = playerBite;
        if (playerBiteAudio.isPlaying == false)
        {
            playerBiteAudio.Play();
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
        uIAudio.PlayOneShot(selectButton);
    }

    //Music 
    public void PlayGameplayMusic()
    {
        musicAudio.clip = gameplayMusic;
        musicAudio.Play();
    }

    public void StopGameplayMusic()
    {
        musicAudio.Stop();
    }
}
