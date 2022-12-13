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
    public Slider dashRechargeSlider;

    [Header("Images")]
    public GameObject dashRechargeBackground;

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

    float dashRechargeCount;

    float startX;
    [HideInInspector] public float savedX;
    RectTransform currentTransform;

    public PlayerController player;

    private void Start()
    {
        currentTransform = hungerBarSlider.GetComponent(typeof(RectTransform)) as RectTransform;
        startX = currentTransform.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayEvolutionPoints();
        DisplayHungerBar();
        DisplayDashRechargeBar();
        DisplayDash();
        DisplayHealth();
    }

    void DisplayEvolutionPoints()
    {
        evolutionPointCount = player.evolutionPoints;

        evolutionPointCountText.text = "Evo Points: (" + evolutionPointCount.ToString() + ")";
    }

    public void IncreaseHungerBarSize(int size)
    {
        currentTransform = hungerBarSlider.GetComponent(typeof(RectTransform)) as RectTransform;
        RectTransform rectTransform = hungerBarSlider.GetComponent(typeof (RectTransform)) as RectTransform;
        float currentSizeX = currentTransform.sizeDelta.x;
        rectTransform.sizeDelta = new Vector2(currentSizeX += size, 90);
        savedX = currentSizeX;
    }

    public void ResetHungerBar()
    {
        currentTransform.sizeDelta = new Vector2(startX, 90);
    }

    void DisplayHungerBar()
    {
        hungerBarSlider.maxValue = player.baseStamina;       
        staminaCount = player.stamina;

        hungerBarSlider.value = staminaCount;
    }

    void DisplayDashRechargeBar()
    {
        dashRechargeSlider.maxValue = player.maxDashChargeTime;

        if (dashRechargeSlider.value <= 0)
        {
            dashRechargeBackground.SetActive(false);
        }
        else
        {
            dashRechargeBackground.SetActive(true);
        }

        dashRechargeCount = player.currentDashChargeTime;

        dashRechargeSlider.value = dashRechargeCount;
    }

    void DisplayDash()
    {
        if (player.dashCharges >= 1) dashOne.SetActive(true);
        else dashOne.SetActive(false);
        if (player.dashCharges >= 2) dashTwo.SetActive(true);
        else dashTwo.SetActive(false);
        if (player.dashCharges >= 3) dashThree.SetActive(true);
        else dashThree.SetActive(false);
    }

    void DisplayHealth()
    {
        if (player.health >= 1) heartOne.SetActive(true);
        else heartOne.SetActive(false);
        if (player.health >= 2) heartTwo.SetActive(true);
        else heartTwo.SetActive(false);
        if (player.health >= 3) heartThree.SetActive(true);
        else heartThree.SetActive(false);
    }
}
