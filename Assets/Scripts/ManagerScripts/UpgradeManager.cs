using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public PlayerController player;
    public GameplayHud gameplayHud;

    [Header("Text")]
    public TextMeshProUGUI swimSpeedText;
    public TextMeshProUGUI turnSpeedText;
    public TextMeshProUGUI stomachCapacityText;

    public TextMeshProUGUI evolutionPointCountText;

    public TextMeshProUGUI evolveText;

    [Header("GameObjects")]
    public GameObject swimSpeedObj;
    public GameObject turnSpeedObj;
    public GameObject stomachCapacityObj;

    public GameObject evolutionObj;

    [Header("Prices - Capacity [3]")]
    public List<int> swimSpeedPrices = new List<int>();
    public List<int> turnSpeedPrices = new List<int>();
    public List<int> stomachCapacityPrices = new List<int>();

    [Header("Pips")]
    GameObject SwimSpeedPips;
    GameObject TurnSpeedPips;
    GameObject StomichCapacityPips;

    public int currentSwimSpeedButtonState = 0;
    public int currentTurnSpeedButtonState = 0;
    public int currentStomachCapacityButtonState = 0;

    public int currentSwimSpeedButtonPriceState = 0;
    public int currentTurnSpeedButtonPriceState = 0;
    public int currentStomachCapacityButtonPriceState = 0;
    public int currentEvolveButtonStateCap = 0;

    [HideInInspector] public int evolutionStage = 0;

    int swimSpeedButtonStateCap;
    int turnSpeedButtonStateCap;
    int stomachCapacityButtonStateCap;

    int swimSpeedButtonPriceStateCap;
    int turnSpeedButtonPriceStateCap;
    int stomachCapacityButtonPriceStateCap;

    int evolutionPointCount;

    bool evolved = false;
    // Update is called once per frame

    private void Start()
    {

    }

    void Update()
    {
        PipManager();
        DisplayText();
        UpgradeButtonManager();

    }

    void DisplayText()
    {
        evolutionPointCount = player.evolutionPoints;

        evolutionPointCountText.text = "Evo Points: (" + evolutionPointCount.ToString() + ")";

        // Upgrades Text
        swimSpeedText.text = "Swim Speed\n (" + swimSpeedPrices[currentSwimSpeedButtonPriceState].ToString() + ") Evo Points";
        turnSpeedText.text = "Turn Speed\n (" + turnSpeedPrices[currentTurnSpeedButtonPriceState].ToString() + ") Evo Points";
        stomachCapacityText.text = "Stomach Capacity\n (" + stomachCapacityPrices[currentStomachCapacityButtonPriceState].ToString() + ") Evo Points";

        // EvolvedButton
        if (evolutionStage == 0)
        {
            if (currentSwimSpeedButtonPriceState == 4)
            {
                swimSpeedText.text = "Swim Speed\n (Max)";
            }
            if (currentTurnSpeedButtonPriceState == 4)
            {
                turnSpeedText.text = "Turn Speed\n (Max)";
            }
            if (currentStomachCapacityButtonPriceState == 4)
            {
                stomachCapacityText.text = "Stomach Capacity\n (Max)";
            }
            if (currentSwimSpeedButtonPriceState == 4 && currentTurnSpeedButtonPriceState == 4 && currentStomachCapacityButtonPriceState == 4)
            {
                evolveText.text = "Evolve";
            }
            else evolveText.text = "Locked";
        }

        else if (evolutionStage == 1)
        {
            if (currentSwimSpeedButtonPriceState == 8)
            {
                swimSpeedText.text = "Swim Speed\n (Max)";
            }
            if (currentTurnSpeedButtonPriceState == 8)
            {
                turnSpeedText.text = "Turn Speed\n (Max)";
            }
            if (currentStomachCapacityButtonPriceState == 8)
            {
                stomachCapacityText.text = "Stomach Capacity\n (Max)";
            }
            if (currentSwimSpeedButtonPriceState == 8 && currentTurnSpeedButtonPriceState == 8 && currentStomachCapacityButtonPriceState == 8)
            {
                evolveText.text = "Evolve";
            }
            else evolveText.text = "Locked";
        }

        else if (evolutionStage == 2)
        {
            if (currentSwimSpeedButtonPriceState == 12)
            {
                swimSpeedText.text = "Swim Speed\n (Max)";
            }
            if (currentTurnSpeedButtonPriceState == 12)
            {
                turnSpeedText.text = "Turn Speed\n (Max)";
            }
            if (currentStomachCapacityButtonPriceState == 12)
            {
                stomachCapacityText.text = "Stomach Capacity\n (Max)";
            }

            evolveText.text = "Evolve (Max)";
        }
    }

    void PipManager()
    {
        for (int i = 0; i < 5; i++)
        {
            swimSpeedObj.transform.GetChild(i).gameObject.SetActive(false);
            turnSpeedObj.transform.GetChild(i).gameObject.SetActive(false);
            stomachCapacityObj.transform.GetChild(i).gameObject.SetActive(false);
            evolutionObj.transform.GetChild(i).gameObject.SetActive(false);
        }

        
        //---------------------------------------------------------------------------------------------
        
        //Set Yellow

        for (int i = 0; i < currentSwimSpeedButtonState; i++)
        {
            swimSpeedObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;
        }

        for (int i = 0; i < currentTurnSpeedButtonState; i++)
        {
            turnSpeedObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;
        }

        for (int i = 0; i < currentStomachCapacityButtonState; i++)
        {
            stomachCapacityObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;
        }

        for (int i = 0; i < evolutionStage; i++)
        {
            evolutionObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.yellow;
        }

        //Set White
        
        if (evolved)
        {
            for (int i = 0; i < currentSwimSpeedButtonState; i++)
            {
                swimSpeedObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }

            for (int i = 0; i < currentTurnSpeedButtonState; i++)
            {
                turnSpeedObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }

            for (int i = 0; i < currentStomachCapacityButtonState; i++)
            {
                stomachCapacityObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }

            for (int i = 0; i < evolutionStage; i++)
            {
                evolutionObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void SwimSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (currentSwimSpeedButtonPriceState < 4)
            {
                if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButtonPriceState])
                {
                    player.baseSwimSpeed += 20;
                    player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButtonPriceState];
                    currentSwimSpeedButtonState += 1;
                    currentSwimSpeedButtonPriceState += 1;
                }
            }
        }
        else if (evolutionStage == 1)
        {
            if (currentSwimSpeedButtonPriceState < 8)
            {
                if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButtonPriceState])
                {
                    player.baseSwimSpeed += 20;
                    player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButtonPriceState];
                    currentSwimSpeedButtonState += 1;
                    currentSwimSpeedButtonPriceState += 1;
                }
            }
        }
        else if (evolutionStage == 2)
        {
            if (currentSwimSpeedButtonPriceState < 12)
            {
                if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButtonPriceState])
                {
                    player.baseSwimSpeed += 20;
                    player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButtonPriceState];
                    currentSwimSpeedButtonState += 1;
                    currentSwimSpeedButtonPriceState += 1;
                }
            }
        }
    }

    public void TurnSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (currentTurnSpeedButtonPriceState < 4)
            {
                if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButtonPriceState])
                {
                    player.baseRotateSpeed += 20;
                    player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButtonPriceState];
                    currentTurnSpeedButtonState += 1;
                    currentTurnSpeedButtonPriceState += 1;
                }
            }
        }
        else if (evolutionStage == 1)
        {
            if (currentTurnSpeedButtonPriceState < 8)
            {
                if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButtonPriceState])
                {
                    player.baseRotateSpeed += 20;
                    player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButtonPriceState];
                    currentTurnSpeedButtonState += 1;
                    currentTurnSpeedButtonPriceState += 1;
                }
            }
        }
        else if (evolutionStage == 2)
        {
            if (currentTurnSpeedButtonPriceState < 12)
            {
                if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButtonPriceState])
                {
                    player.baseRotateSpeed += 20;
                    player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButtonPriceState];
                    currentTurnSpeedButtonState += 1;
                    currentTurnSpeedButtonPriceState += 1;
                }
            }
        }
    }

    public void StomachCapacityUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (currentStomachCapacityButtonPriceState < 4)
            {
                if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButtonPriceState])
                {
                    player.baseStamina += 20;
                    gameplayHud.IncreaseHungerBarSize(20);
                    player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButtonPriceState];
                    currentStomachCapacityButtonState += 1;
                    currentStomachCapacityButtonPriceState += 1;
                }
            }
        }
        else if (evolutionStage == 1)
        {
            if (currentStomachCapacityButtonPriceState < 8)
            {
                if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButtonPriceState])
                {
                    player.baseStamina += 20;
                    gameplayHud.IncreaseHungerBarSize(20);
                    player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButtonPriceState];
                    currentStomachCapacityButtonState += 1;
                    currentStomachCapacityButtonPriceState += 1;
                }
            }
        }
        else if (evolutionStage == 2)
        {
            if (currentStomachCapacityButtonPriceState < 12)
            {
                if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButtonPriceState])
                {
                    player.baseStamina += 20;
                    gameplayHud.IncreaseHungerBarSize(20);
                    player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButtonPriceState];
                    currentStomachCapacityButtonState += 1;
                    currentStomachCapacityButtonPriceState += 1;
                }
            }
        }
    }

    public void EvolveButton()
    {
        if (evolutionStage == 0)
        {
            if (currentSwimSpeedButtonPriceState == 4 && currentTurnSpeedButtonPriceState == 4 && currentStomachCapacityButtonPriceState == 4)
            {
                Debug.Log("evolve");
                player.baseHealth += 1;
                player.baseDashCharges += 1;
                evolved = true;
                evolutionStage += 1;
            }
        }

        else if (evolutionStage == 1)
        {
            if (currentSwimSpeedButtonPriceState == 8 && currentTurnSpeedButtonPriceState == 8 && currentStomachCapacityButtonPriceState == 8)
            {
                player.baseHealth += 1;
                player.baseDashCharges += 1;
                evolved = true;
                evolutionStage += 1;
            }
        }
    }

    public void UpgradeButtonManager()
    {
        currentEvolveButtonStateCap = 2;

        if (evolved)
        {
            currentSwimSpeedButtonState = 0;
            currentTurnSpeedButtonState = 0;
            currentStomachCapacityButtonState = 0;
            evolved = false;
        }

        for (int i = 0; i < currentEvolveButtonStateCap; i++)
        {
            evolutionObj.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (evolutionStage == 0)
        {
            swimSpeedButtonStateCap = 4;
            turnSpeedButtonStateCap = 4;
            stomachCapacityButtonStateCap = 4;

            swimSpeedButtonPriceStateCap = 4;
            turnSpeedButtonPriceStateCap = 4;
            stomachCapacityButtonPriceStateCap = 4;

            for (int i = 0; i < swimSpeedButtonStateCap; i++)
            {
                swimSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < turnSpeedButtonStateCap; i++)
            {
                turnSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < stomachCapacityButtonStateCap; i++)
            {
                stomachCapacityObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        else if (evolutionStage == 1)
        {
            if (evolved)
            {
                currentSwimSpeedButtonState = 0;
                currentTurnSpeedButtonState = 0;
                currentStomachCapacityButtonState = 0;
                evolved = false;
            }

            swimSpeedButtonStateCap = 4;
            turnSpeedButtonStateCap = 4;
            stomachCapacityButtonStateCap = 4;

            swimSpeedButtonPriceStateCap = 8;
            turnSpeedButtonPriceStateCap = 8;
            stomachCapacityButtonPriceStateCap = 8;


            for (int i = 0; i < swimSpeedButtonStateCap; i++)
            {
                swimSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < turnSpeedButtonStateCap; i++)
            {
                turnSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < stomachCapacityButtonStateCap; i++)
            {
                stomachCapacityObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        else if (evolutionStage == 2)
        {
            if (evolved)
            {
                currentSwimSpeedButtonState = 0;
                currentTurnSpeedButtonState = 0;
                currentStomachCapacityButtonState = 0;
                evolved = false;
            }

            swimSpeedButtonStateCap = 4;
            turnSpeedButtonStateCap = 4;
            stomachCapacityButtonStateCap = 4;

            swimSpeedButtonPriceStateCap = 12;
            turnSpeedButtonPriceStateCap = 12;
            stomachCapacityButtonPriceStateCap = 12;


            for (int i = 0; i < swimSpeedButtonStateCap; i++)
            {
                swimSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < turnSpeedButtonStateCap; i++)
            {
                turnSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < stomachCapacityButtonStateCap; i++)
            {
                stomachCapacityObj.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void ResetUpgrades()
    {
        for (int i = 0; i < 5; i++)
        {
            swimSpeedObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
        }

        for (int i = 0; i < 5; i++)
        {
            turnSpeedObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
        }

        for (int i = 0; i < 5; i++)
        {
            stomachCapacityObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
        }

        for (int i = 0; i < 5; i++)
        {
            evolutionObj.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
        }

        currentSwimSpeedButtonState = 0;
        currentTurnSpeedButtonState = 0;
        currentStomachCapacityButtonState = 0;

        currentSwimSpeedButtonPriceState = 0;
        currentTurnSpeedButtonPriceState = 0;
        currentStomachCapacityButtonPriceState = 0;
        currentEvolveButtonStateCap = 0;

        evolutionStage = 0;

        swimSpeedButtonStateCap = 0;
        turnSpeedButtonStateCap = 0;
        stomachCapacityButtonStateCap = 0;

        swimSpeedButtonPriceStateCap = 0;
        turnSpeedButtonPriceStateCap = 0;
        stomachCapacityButtonPriceStateCap = 0;

        evolutionPointCount = 0;

        evolved = false;
    }
}
