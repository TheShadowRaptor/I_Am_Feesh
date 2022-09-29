using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public GameObject warningRadius;

    public bool spottedPlayer = false;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spottedPlayer = true;
            warningRadius.SetActive(true);
        }
    }
}
