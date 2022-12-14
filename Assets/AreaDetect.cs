using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetect : MonoBehaviour
{

    public int areaNum = 0;
    int currentAreaNum = 0;

    GameObject playerObj;
    PlayerController player;

    GameObject audioManagerObj;
    AudioManager audioManager;

    private void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();

        }

        if (audioManager == null)
        {
            audioManagerObj = GameObject.Find("AudioManager");
            audioManager = audioManagerObj.GetComponent<AudioManager>();
        }
         
        else
        {
            switch (currentAreaNum)
            {
                case 0:
                    player.tallyBiome = "Shallow";
                    break;

                case 1:
                    player.tallyBiome = "CoralReef";
                    break;

                case 2:
                    player.tallyBiome = "OpenOcean";
                    break;

                case 3:
                    player.tallyBiome = "TwilightZone";
                    break;

                case 4:
                    player.tallyBiome = "Abyss";
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentAreaNum = areaNum;
            if (currentAreaNum == 0)
            {
                audioManager.PlayGameplayMusicShallow();
                audioManager.PlayBattleMusicShallow();
            }
            else if (currentAreaNum == 1)
            {
                audioManager.PlayGameplayMusicCoralReef();
                audioManager.PlayBattleMusicCoralReef();
            }

            else if (currentAreaNum == 2)
            {
                audioManager.PlayGameplayMusicOpenOcean();
                audioManager.PlayBattleMusicOpenOcean();
            }

            else if (currentAreaNum == 3)
            {
                audioManager.PlayGameplayMusicTwilight();
                audioManager.PlayBattleMusicTwilight();
            }

            else if (currentAreaNum == 4) audioManager.StopGameplayMusic();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
