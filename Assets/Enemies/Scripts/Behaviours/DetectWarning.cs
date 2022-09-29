using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWarning : MonoBehaviour
{
    public bool spottedWarning = false;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Warning"))
        {
            spottedWarning = true;
        }
    }
}
