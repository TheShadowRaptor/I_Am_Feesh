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
    public int currentSwimSpeedButton;
    public int currentTurnSpeedButton;
    public int currentStomachCapacityButton;

    public int currentDepthIncreaseButton;
    public int currentDashSpeedButton;

    public int evolutionStage;
}
