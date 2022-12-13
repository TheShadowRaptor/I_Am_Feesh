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

    // Update is called once per frame
    void Update()
    {
        musicVolume = musicSlider.value;
        soundVolume = soundSlider.value;
    }
}
