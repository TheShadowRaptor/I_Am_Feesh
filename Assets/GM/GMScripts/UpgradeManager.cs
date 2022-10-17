using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    GameObject playerObj;
    PlayerController player;

    [Header("Text")]
    public TextMeshProUGUI SwimSpeedText;
    public TextMeshProUGUI TurnSpeedText;
    public TextMeshProUGUI StomachCapacityText;

    [Header("Prices - Capacity [3]")]
    public List<int> swimSpeedPrices = new List<int>(3);
    public List<int> turnSpeedPrices = new List<int>(3);
    public List<int> stomachCapacityPrices = new List<int>(3);

    int currentSwimSpeedButton = 0;
    int currentTurnSpeedButton = 0;
    int currentStomachCapacityButton = 0;
    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        

        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerController>();

        DisplayText();
        UpgradePriceManager();
    }

    void DisplayText()
    {
        SwimSpeedText.text = "Swim Speed\n (" + swimSpeedPrices[currentSwimSpeedButton].ToString() + ")";       
        TurnSpeedText.text = "Turn Speed\n (" + turnSpeedPrices[currentTurnSpeedButton].ToString() + ")";
        StomachCapacityText.text = "Stomach Capacity\n (" + stomachCapacityPrices[currentStomachCapacityButton].ToString() + ")";
    }

    void UpgradePriceManager()
    {
        // Manage Speed
    }

    public void SwimSpeedUpgradeButton()
    {
        if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButton])
        {
            player.startSwimSpeed += 10;
            player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButton];
            currentSwimSpeedButton += 1;
        }
    }

    public void TurnSpeedUpgradeButton()
    {
        if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButton])
        {
            player.startRotateSpeed += 10;
            player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButton];
            currentTurnSpeedButton += 1;
        }      
    }

    public void StomachCapacityUpgradeButton()
    {
        if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButton])
        {
            player.startRotateSpeed += 10;
            player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButton];
            currentStomachCapacityButton += 1;
        }
    }

    public void EvolveButton()
    {

    }
}
