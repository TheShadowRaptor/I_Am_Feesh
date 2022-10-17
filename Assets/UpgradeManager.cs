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

    [Header("Prices")]
    public int swimSpeedPrice;
    public int turnSpeedPrice;
    public int stomachCapacityPrice;

    // Update is called once per frame
    void Update()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerController>();

        DisplayText();
        UpgradePriceManager();
    }

    void DisplayText()
    {
        SwimSpeedText.text = "Swim Speed\n (" + swimSpeedPrice.ToString() + ")";
        TurnSpeedText.text = "Turn Speed\n (" + turnSpeedPrice.ToString() + ")";
        StomachCapacityText.text = "Stomach Capacity\n (" + stomachCapacityPrice.ToString() + ")";
    }

    void UpgradePriceManager()
    {
        // Manage Speed
    }

    public void SwimSpeedUpgradeButton()
    {
        player.startSwimSpeed += 10; 
        player.swimSpeed += 10; 
    }

    public void TurnSpeedUpgradeButton()
    {
        player.startRotateSpeed += 10;
        player.rotateSpeed += 10;
    }

    public void StomachCapacityUpgradeButton()
    {
        player.startStamina += 5;
        player.stamina += 5;
    }

    public void EvolveButton()
    {

    }
}
