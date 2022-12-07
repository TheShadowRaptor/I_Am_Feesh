using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetect : MonoBehaviour
{

    public int areaNum = 0;
    int currentAreaNum = 0;
    GameObject playerObj;
    PlayerController player;

    private void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();

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
            Debug.Log("Collision");
            currentAreaNum = areaNum;
        }
    }
}
