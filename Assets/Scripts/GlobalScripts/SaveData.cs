using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    // PlayerData
    public int evolutionPoints;
    public int health;
    public float stamina;
    public int damage;
    public int dashCharges;
    public int dashSpeed;
    public float swimSpeed;
    public float rotateSpeed;
    public int depthLimit;

    // UpgradeData
    public int currentSwimSpeedButtonState;
    public int currentTurnSpeedButtonState;
    public int currentStomachCapacityButtonState;
    public int currentEvolveButtonStateCap;

    public int currentSwimSpeedButtonPriceState;
    public int currentTurnSpeedButtonPriceState;
    public int currentStomachCapacityPriceButtonState;

    public int evolutionStage;

    // GameplayHudData
    public float savedX;
}
