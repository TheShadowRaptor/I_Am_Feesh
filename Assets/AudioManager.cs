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

    private void Start()
    {
        playerAudio = gameObject.transform.Find("PlayerAudio").gameObject.GetComponent<AudioSource>();
        playerBiteAudio = gameObject.transform.Find("PlayerBiteAudio").gameObject.GetComponent<AudioSource>();
        objectAudio = gameObject.transform.Find("ObjectAudio").gameObject.GetComponent<AudioSource>();
        enemyAudio = gameObject.transform.Find("EnemyAudio").gameObject.GetComponent<AudioSource>();
        uIAudio = gameObject.transform.Find("UiAudio").gameObject.GetComponent<AudioSource>();
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
        playerAudio.PlayOneShot(enemyBite);
    }

    //UI Sounds
    public void PlaySelectButton()
    {
        uIAudio.clip = selectButton;
        uIAudio.PlayOneShot(selectButton);
    }
}
