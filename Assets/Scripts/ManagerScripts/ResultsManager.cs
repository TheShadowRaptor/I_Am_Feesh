using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsManager : MonoBehaviour
{
    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI deathText;
    GameObject playerObj;
    PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }

        if (deathText != null)
        {
            deathText.text = player.causeOfDeath;
        }
        resultsText.text = "Evo Points Collected: " + player.tallyEvoPoints +
            "\nFood Eaten: " + player.tallyFoodEaten +
            "\nFish Killed: " + player.tallyKills +
            "\nBiome Reached: " + player.tallyBiome;

    }
}
