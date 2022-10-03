using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMananger : MonoBehaviour
{
    public TextMeshProUGUI evolutionPointCountText;
    public Slider hungerBarSlider;

    int evolutionPointCount;
    float staminaCount;
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
        DisplayEvolutionPoints();
        DisplayHungerBar();
    }

    void DisplayEvolutionPoints()
    {
        evolutionPointCount = player.playerEvolutionPoints;

        evolutionPointCountText.text = "Evo Points: (" + evolutionPointCount.ToString() + ")";
    }

    void DisplayHungerBar()
    {
        hungerBarSlider.maxValue = player.startStamina;
        staminaCount = player.playerStamina;

        hungerBarSlider.value = staminaCount;
    }
}
