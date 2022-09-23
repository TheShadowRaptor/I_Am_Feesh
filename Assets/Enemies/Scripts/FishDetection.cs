using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDetection : MonoBehaviour
{
    public bool SpottedPlayer = false;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SpottedPlayer = true;
        }
    }
}
