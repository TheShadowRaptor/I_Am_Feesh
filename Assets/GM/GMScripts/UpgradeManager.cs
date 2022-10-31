using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    GameObject playerObj;
    PlayerController player;

    [Header("Text")]
    public TextMeshProUGUI swimSpeedText;
    public TextMeshProUGUI turnSpeedText;
    public TextMeshProUGUI stomachCapacityText;

    public TextMeshProUGUI depthIncreaseText;
    public TextMeshProUGUI dashSpeedText;

    public TextMeshProUGUI evolutionPointCountText;

    public TextMeshProUGUI evolveText;

    [Header("GameObjects")]
    public GameObject swimSpeedObj;
    public GameObject turnSpeedObj;
    public GameObject stomachCapacityObj;

    public GameObject depthIncreaseObj;
    public GameObject dashSpeedObj;

    [Header("Prices - Capacity [3]")]
    public List<int> swimSpeedPrices = new List<int>();
    public List<int> turnSpeedPrices = new List<int>();
    public List<int> stomachCapacityPrices = new List<int>();

    public List<int> depthIncreasePrices = new List<int>();
    public List<int> dashSpeedPrices = new List<int>();

    [HideInInspector] public int currentSwimSpeedButton = 0;
    [HideInInspector] public int currentTurnSpeedButton = 0;
    [HideInInspector] public int currentStomachCapacityButton = 0;

    [HideInInspector] public int currentDepthIncreaseButton = 0;
    [HideInInspector] public int currentDashSpeedButton = 0;

    [HideInInspector] public int evolutionStage = 0;

    int evolutionPointCount;
    // Update is called once per frame

    private void Start()
    {
        swimSpeedObj.SetActive(false);
        turnSpeedObj.SetActive(false); 
        stomachCapacityObj.SetActive(false); 

        depthIncreaseObj.SetActive(false); 
        dashSpeedObj.SetActive(false); 
}

    void Update()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerController>();

        DisplayText();
        UpgradeButtonManager(evolutionStage);
    }

    void DisplayText()
    {
        evolutionPointCount = player.evolutionPoints;

        evolutionPointCountText.text = "Evo Points: (" + evolutionPointCount.ToString() + ")";

        // Upgrades Text
        swimSpeedText.text = "Swim Speed\n (" + swimSpeedPrices[currentSwimSpeedButton].ToString() + ")";
        turnSpeedText.text = "Turn Speed\n (" + turnSpeedPrices[currentTurnSpeedButton].ToString() + ")";
        stomachCapacityText.text = "Stomach Capacity\n (" + stomachCapacityPrices[currentStomachCapacityButton].ToString() + ")";

        depthIncreaseText.text = "Depth Increase\n (" + depthIncreasePrices[currentDepthIncreaseButton].ToString() + ")";
        dashSpeedText.text = "Dash Speed\n (" + dashSpeedPrices[currentDashSpeedButton].ToString() + ")";

        // EvolvedButton
        if (evolutionStage == 0)
        {
            if (currentSwimSpeedButton == 4 && currentTurnSpeedButton == 4 && currentStomachCapacityButton == 4)
            {
                evolveText.text = "Evolve";
            }
            else evolveText.text = "Locked";
        }

        else if (evolutionStage == 1)
        {
            if (currentSwimSpeedButton == 8 && currentTurnSpeedButton == 8 && currentStomachCapacityButton == 8)
            {
                evolveText.text = "Evolve";
            }
            else evolveText.text = "Locked";
        }
    }

    public void SwimSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButton])
            {
                player.baseSwimSpeed += 10;
                player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButton];
                currentSwimSpeedButton += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButton])
            {
                player.baseSwimSpeed += 10;
                player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButton];
                currentSwimSpeedButton += 1;
            }
        }
    }

    public void TurnSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButton])
            {
                player.baseRotateSpeed += 10;
                player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButton];
                currentTurnSpeedButton += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButton])
            {
                player.baseRotateSpeed += 10;
                player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButton];
                currentTurnSpeedButton += 1;
            }
        }
    }

    public void StomachCapacityUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButton])
            {
                player.baseRotateSpeed += 20;
                player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButton];
                currentStomachCapacityButton += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButton])
            {
                player.baseRotateSpeed += 10;
                player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButton];
                currentStomachCapacityButton += 1;
            }
        }
    }

    public void DepthIncreaseUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= depthIncreasePrices[currentDepthIncreaseButton])
            {
                player.baseDepthLimit += 1;
                player.evolutionPoints -= depthIncreasePrices[currentDepthIncreaseButton];
                currentDepthIncreaseButton += 1;
            }
        }
        else if (evolutionStage == 1)
        {

            if (player.evolutionPoints >= depthIncreasePrices[currentDepthIncreaseButton])
            {
                player.baseDashSpeed += 1;
                player.evolutionPoints -= stomachCapacityPrices[currentDepthIncreaseButton];
                currentDepthIncreaseButton += 1;
            }
        }
    }

    public void DashSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= dashSpeedPrices[currentDashSpeedButton])
            {
                player.baseDashSpeed += 10;
                player.evolutionPoints -= dashSpeedPrices[currentDashSpeedButton];
                currentDashSpeedButton += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= dashSpeedPrices[currentDashSpeedButton])
            {
                player.baseDashSpeed += 10;
                player.evolutionPoints -= dashSpeedPrices[currentDashSpeedButton];
                currentDashSpeedButton += 1;
            }
        }
    }


    public void EvolveButton(int evo)
    {
        if (evo == 0)
        {
            if (currentSwimSpeedButton == 4 && currentTurnSpeedButton == 4 && currentStomachCapacityButton == 4)
            {
                player.baseHealth += 1;
                player.baseDashCharges += 1;
                evo += 1;
            }
        }

        else if (evo == 1)
        {
            if (currentSwimSpeedButton == 8 && currentTurnSpeedButton == 8 && currentStomachCapacityButton == 8)
            {
                player.baseHealth += 1;
                player.baseDashCharges += 1;
                evo += 1;
            }
        }
    }

    public void UpgradeButtonManager(int evo)
    {
        if (evo == 0)
        {
            if (currentSwimSpeedButton == 4) swimSpeedObj.SetActive(false);
            else swimSpeedObj.SetActive(true);

            if (currentTurnSpeedButton == 4) turnSpeedObj.SetActive(false);
            else turnSpeedObj.SetActive(true);

            if (currentStomachCapacityButton == 4) stomachCapacityObj.SetActive(false);
            else stomachCapacityObj.SetActive(true);
        }

        else if (evo <= 1)
        {
            if (currentSwimSpeedButton == 8) swimSpeedObj.SetActive(false);
            else swimSpeedObj.SetActive(true);

            if (currentTurnSpeedButton == 8) turnSpeedObj.SetActive(false);
            else turnSpeedObj.SetActive(true);

            if (currentStomachCapacityButton == 8) stomachCapacityObj.SetActive(false);
            else stomachCapacityObj.SetActive(true);

            if (currentDepthIncreaseButton == 1) depthIncreaseObj.SetActive(false);
            else depthIncreaseObj.SetActive(true);

            if (currentDashSpeedButton == 2) dashSpeedObj.SetActive(false);
            else dashSpeedObj.SetActive(true);
        }
    }
}
