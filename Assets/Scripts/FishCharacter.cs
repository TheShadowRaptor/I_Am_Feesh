using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : GameCharacter
{
    [Header("FishStats")]
    public float swimSpeed = 20;
    public float fleeSwimSpeed = 40;

    [Header("Scripts")]
    public DetectPlayer detectPlayer;
    public DetectWarning warningBehaviour;

    [Header("GameObjects")]
    public GameObject food;
    protected GameObject player;

    public bool PlayerSpotted()
    {
        // If player is spotted becomes true
        if (detectPlayer.spottedPlayer)
        {
            return true;
        }
        return false;
    }   
}
