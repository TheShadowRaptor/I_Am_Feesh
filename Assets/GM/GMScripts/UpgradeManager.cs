using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public GameObject evolutionObj;

    [Header("Prices - Capacity [3]")]
    public List<int> swimSpeedPrices = new List<int>();
    public List<int> turnSpeedPrices = new List<int>();
    public List<int> stomachCapacityPrices = new List<int>();

    public List<int> depthIncreasePrices = new List<int>();
    public List<int> dashSpeedPrices = new List<int>();

    [Header("Pips")]
    GameObject SwimSpeedPips;
    GameObject TurnSpeedPips;
    GameObject StomichCapacityPips;

    public int currentSwimSpeedButtonState = 0;
    public int currentTurnSpeedButtonState = 0;
    public int currentStomachCapacityButtonState = 0;
    [HideInInspector] public int currentDepthIncreaseButtonState = 0;
    [HideInInspector] public int currentDashSpeedButtonState = 0;

    public int currentSwimSpeedButtonPriceState = 0;
    public int currentTurnSpeedButtonPriceState = 0;
    public int currentStomachCapacityButtonPriceState = 0;
    [HideInInspector] public int currentDepthIncreaseButtonPriceState = 0;
    [HideInInspector] public int currentDashSpeedButtonPriceState = 0;

    [HideInInspector] public int evolutionStage = 0;

    int swimSpeedButtonStateCap;
    int turnSpeedButtonStateCap;
    int stomachCapacityButtonStateCap;
    int depthIncreaseButtonStateCap;
    int dashSpeedButtonStateCap;

    int swimSpeedButtonPriceStateCap;
    int turnSpeedButtonPriceStateCap;
    int stomachCapacityButtonPriceStateCap;
    int depthIncreaseButtonPriceStateCap;
    int dashSpeedButtonPriceStateCap;

    int evolutionPointCount;

    bool evolved = false;
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
        PipManager();

        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }
      
        DisplayText();
        UpgradeButtonManager();

    }

    void DisplayText()
    {
        evolutionPointCount = player.evolutionPoints;

        evolutionPointCountText.text = "Evo Points: (" + evolutionPointCount.ToString() + ")";

        // Upgrades Text
        swimSpeedText.text = "Swim Speed\n (" + swimSpeedPrices[currentSwimSpeedButtonPriceState].ToString() + ")";
        turnSpeedText.text = "Turn Speed\n (" + turnSpeedPrices[currentTurnSpeedButtonPriceState].ToString() + ")";
        stomachCapacityText.text = "Stomach Capacity\n (" + stomachCapacityPrices[currentStomachCapacityButtonPriceState].ToString() + ")";

        depthIncreaseText.text = "Depth Increase\n (" + depthIncreasePrices[currentDepthIncreaseButtonPriceState].ToString() + ")";
        dashSpeedText.text = "Dash Speed\n (" + dashSpeedPrices[currentDashSpeedButtonPriceState].ToString() + ")";

        // EvolvedButton
        if (evolutionStage == 0)
        {
            if (currentSwimSpeedButtonPriceState == 4 && currentTurnSpeedButtonPriceState == 4 && currentStomachCapacityButtonPriceState == 4)
            {
                evolveText.text = "Evolve";
            }
            else evolveText.text = "Locked";
        }

        else if (evolutionStage == 1)
        {
            if (currentSwimSpeedButtonPriceState == 8 && currentTurnSpeedButtonPriceState == 8 && currentStomachCapacityButtonPriceState == 8)
            {
                evolveText.text = "Evolve";
            }
            else evolveText.text = "Locked";
        }
    }

    void PipManager()
    {
        for (int i = 0; i < 5; i++)
        {
            swimSpeedObj.transform.GetChild(i).gameObject.SetActive(false);
            turnSpeedObj.transform.GetChild(i).gameObject.SetActive(false);
            stomachCapacityObj.transform.GetChild(i).gameObject.SetActive(false);
            depthIncreaseObj.transform.GetChild(i).gameObject.SetActive(false);
            dashSpeedObj.transform.GetChild(i).gameObject.SetActive(false);
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
            if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButtonPriceState])
            {
            player.baseSwimSpeed += 10;
            player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButtonPriceState];
            currentSwimSpeedButtonState += 1;
            currentSwimSpeedButtonPriceState += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= swimSpeedPrices[currentSwimSpeedButtonPriceState])
            {
                player.baseSwimSpeed += 10;
                player.evolutionPoints -= swimSpeedPrices[currentSwimSpeedButtonPriceState];
                currentSwimSpeedButtonState += 1;
                currentSwimSpeedButtonPriceState += 1;
            }
        }
    }

    public void TurnSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButtonPriceState])
            {
                player.baseRotateSpeed += 10;
                player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButtonPriceState];
                currentTurnSpeedButtonState += 1;
                currentTurnSpeedButtonPriceState += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= turnSpeedPrices[currentTurnSpeedButtonPriceState])
            {
                player.baseRotateSpeed += 10;
                player.evolutionPoints -= turnSpeedPrices[currentTurnSpeedButtonPriceState];
                currentTurnSpeedButtonState += 1;
                currentTurnSpeedButtonPriceState += 1;
            }
        }
    }

    public void StomachCapacityUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButtonPriceState])
            {
                player.baseRotateSpeed += 20;
                player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButtonPriceState];
                currentStomachCapacityButtonState += 1;
                currentStomachCapacityButtonPriceState += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= stomachCapacityPrices[currentStomachCapacityButtonPriceState])
            {
                player.baseRotateSpeed += 10;
                player.evolutionPoints -= stomachCapacityPrices[currentStomachCapacityButtonPriceState];
                currentStomachCapacityButtonState += 1;
                currentStomachCapacityButtonPriceState += 1;
            }
        }
    }

    public void DepthIncreaseUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= depthIncreasePrices[currentDepthIncreaseButtonPriceState])
            {
                player.baseDepthLimit += 1;
                player.evolutionPoints -= depthIncreasePrices[currentDepthIncreaseButtonPriceState];
                currentDepthIncreaseButtonState += 1;
                currentDepthIncreaseButtonPriceState += 1;
            }
        }
        else if (evolutionStage == 1)
        {

            if (player.evolutionPoints >= depthIncreasePrices[currentDepthIncreaseButtonPriceState])
            {
                player.baseDashSpeed += 1;
                player.evolutionPoints -= stomachCapacityPrices[currentDepthIncreaseButtonPriceState];
                currentDepthIncreaseButtonState += 1;
                currentDepthIncreaseButtonPriceState += 1;
            }
        }
    }

    public void DashSpeedUpgradeButton()
    {
        if (evolutionStage == 0)
        {
            if (player.evolutionPoints >= dashSpeedPrices[currentDashSpeedButtonPriceState])
            {
                player.baseDashSpeed += 10;
                player.evolutionPoints -= dashSpeedPrices[currentDashSpeedButtonPriceState];
                currentDashSpeedButtonState += 1;
                currentDashSpeedButtonPriceState += 1;
            }
        }
        else if (evolutionStage == 1)
        {
            if (player.evolutionPoints >= dashSpeedPrices[currentDashSpeedButtonPriceState])
            {
                player.baseDashSpeed += 10;
                player.evolutionPoints -= dashSpeedPrices[currentDashSpeedButtonPriceState];
                currentDashSpeedButtonState += 1;
                currentDashSpeedButtonPriceState += 1;
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
        if (evolved)
        {
            currentSwimSpeedButtonState = 0;
            currentTurnSpeedButtonState = 0;
            currentStomachCapacityButtonState = 0;
            evolved = false;
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

            if (currentSwimSpeedButtonState == swimSpeedButtonStateCap) swimSpeedObj.SetActive(false);           
            else swimSpeedObj.SetActive(true);

            if (currentTurnSpeedButtonState == turnSpeedButtonStateCap) turnSpeedObj.SetActive(false);
            else turnSpeedObj.SetActive(true);

            if (currentStomachCapacityButtonState == stomachCapacityButtonStateCap) stomachCapacityObj.SetActive(false);
            else stomachCapacityObj.SetActive(true);

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
            depthIncreaseButtonStateCap = 1;
            dashSpeedButtonStateCap = 2;

            swimSpeedButtonPriceStateCap = 8;
            turnSpeedButtonPriceStateCap = 8;
            stomachCapacityButtonPriceStateCap = 8;
            depthIncreaseButtonPriceStateCap = 1;
            dashSpeedButtonPriceStateCap = 2;


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

            for (int i = 0; i < depthIncreaseButtonStateCap; i++)
            {
                swimSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < dashSpeedButtonStateCap; i++)
            {
                turnSpeedObj.transform.GetChild(i).gameObject.SetActive(true);
            }

            if (currentSwimSpeedButtonState == swimSpeedButtonStateCap) swimSpeedObj.SetActive(false);
            else swimSpeedObj.SetActive(true);

            if (currentTurnSpeedButtonPriceState == turnSpeedButtonPriceStateCap) turnSpeedObj.SetActive(false);
            else turnSpeedObj.SetActive(true);

            if (currentStomachCapacityButtonPriceState == stomachCapacityButtonPriceStateCap) stomachCapacityObj.SetActive(false);
            else stomachCapacityObj.SetActive(true);

            if (currentDepthIncreaseButtonPriceState == depthIncreaseButtonPriceStateCap) depthIncreaseObj.SetActive(false);
            else depthIncreaseObj.SetActive(true);

            if (currentDashSpeedButtonPriceState == dashSpeedButtonPriceStateCap) dashSpeedObj.SetActive(false);
            else dashSpeedObj.SetActive(true);
        }

    }
}
