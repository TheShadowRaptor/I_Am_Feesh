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
    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        deathText.text = player.causeOfDeath;
        resultsText.text = "Evo Points Collected: " + player.tallyEvoPoints +
            "\nFood Eaten: " + player.tallyFoodEaten +
            "\nFish Killed: " + player.tallyKills +
            "\nBiome Reached: " + player.tallyBiome;

    }
}
