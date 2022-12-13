using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider soundSlider;

    public float musicVolume;
    public float soundVolume;

    private void Start()
    {
        musicSlider.value = 0.5f;    
        soundSlider.value = 0.5f;    
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume = musicSlider.value;
        soundVolume = soundSlider.value;
    }
}
