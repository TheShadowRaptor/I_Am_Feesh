using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMananger : MonoBehaviour
{
    public Canvas title;
    public Canvas gameplay;
    public Canvas pause;
    public Canvas results;
    public Canvas upgrade;
    public Canvas win;

    public void ActivateTitle()
    {
        title.enabled = true;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }
    public void ActivateGameplay()
    {
        title.enabled = false;
        gameplay.enabled = true;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void ActivatePause()
    {
        title.enabled = false;
        gameplay.enabled = false;
        pause.enabled = true;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void ActivateResults()
    {
        title.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = true;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void ActivateUpgrade()
    {
        title.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = true;
        win.enabled = false;
    }

    public void ActivateWin()
    {
        title.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = true;
    }
}
