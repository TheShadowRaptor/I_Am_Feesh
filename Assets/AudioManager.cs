using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSourceScript playerAudio;
    public AudioSourceScript playerBiteAudio;
    public AudioSourceScript objectAudio;
    public AudioSourceScript enemyAudio;
    public AudioSourceScript uIAudio;

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

    //Player Sounds
    public void PlayPlayerBite()
    {
        if (playerBiteAudio.audioSource.isPlaying == false)
        {
            playerBiteAudio.Play(playerBite);
        }
    }

    public void PlayPlayerReadyingBite()
    {
        playerAudio.Play(playerReadyingBite);
    }

    public void PlayPlayerDeath()
    {
        playerAudio.Play(playerDeath);
    }

    public void PlayPlayerSwim()
    {
        if (playerAudio.audioSource.isPlaying == false || playerBiteAudio.audioSource.isPlaying == false)
        {
            playerAudio.Play(playerSwim);
        }
    }

    public void PlayPlayerDash()
    {
        playerAudio.Play(playerDash);
    }
    
    //------------------------------------------

    //Object Sounds
    public void PlayFoodEaten()
    {
        objectAudio.Play(foodEaten);
    }

    //Enemy Sounds
    public void PlayEnemyBite()
    {
        if (enemyAudio.audioSource.isPlaying == false)
        {
            enemyAudio.Play(enemyBite);
        }
    }

    //UI Sounds
    public void SelectButton()
    {
        uIAudio.Play(selectButton);
    }
}
