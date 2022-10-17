using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayHud : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI evolutionPointCountText;

    [Header("Slider")]
    public Slider hungerBarSlider;

    [Header("DashCharges")]
    public GameObject dashOne;
    public GameObject dashTwo;
    public GameObject dashThree;

    [Header("Health")]
    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    int evolutionPointCount;
    float staminaCount;

    GameObject playerObj;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player");
            player = playerObj.GetComponent<PlayerController>();
        }

        DisplayEvolutionPoints();
        DisplayHungerBar();
        DisplayDash();
        DisplayHealth();
    }

    void DisplayEvolutionPoints()
    {
        evolutionPointCount = player.playerEvolutionPoints;

        evolutionPointCountText.text = "Evo Points: (" + evolutionPointCount.ToString() + ")";
    }

    void DisplayHungerBar()
    {
        hungerBarSlider.maxValue = player.startStamina;
        staminaCount = player.stamina;

        hungerBarSlider.value = staminaCount;
    }

    void DisplayDash()
    {
        if (player.dashCharges >= 1) dashOne.SetActive(true);
        else dashOne.SetActive(false);
        if (player.dashCharges >= 2) dashTwo.SetActive(true);
        else dashTwo.SetActive(false);
        if (player.dashCharges == 3) dashThree.SetActive(true);
        else dashThree.SetActive(false);
    }

    void DisplayHealth()
    {
        if (player.health >= 1) heartOne.SetActive(true);
        else heartOne.SetActive(false);
        if (player.health >= 2) heartTwo.SetActive(true);
        else heartTwo.SetActive(false);
        if (player.health == 3) heartThree.SetActive(true);
        else heartThree.SetActive(false);
    }
}
