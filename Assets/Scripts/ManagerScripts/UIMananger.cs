using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMananger : MonoBehaviour
{
    public Canvas title;
    public Canvas control;
    public Canvas settings;
    public Canvas gameplay;
    public Canvas pause;
    public Canvas results;
    public Canvas upgrade;
    public Canvas win;

    public void TitleCanvasOn()
    {
        title.enabled = true;
        control.enabled = false;
        settings.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void ControlsCanvasOn()
    {
        title.enabled = false;
        control.enabled = true;
        settings.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void SettingsCanvasOn()
    {
        title.enabled = false;
        control.enabled = false;
        settings.enabled = true;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }
    public void GameplayCanvasOn()
    {
        title.enabled = false;
        control.enabled = false;
        settings.enabled = false;
        gameplay.enabled = true;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void PauseCanvasOn()
    {
        title.enabled = false;
        control.enabled = false;
        settings.enabled = false;
        gameplay.enabled = false;
        pause.enabled = true;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void ResultsCanvasOn()
    {
        title.enabled = false;
        control.enabled = false;
        settings.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = true;
        upgrade.enabled = false;
        win.enabled = false;
    }

    public void UpgradeCanvasOn()
    {
        title.enabled = false;
        control.enabled = false;
        settings.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = true;
        win.enabled = false;
    }

    public void WinCanvasOn()
    {
        title.enabled = false;
        control.enabled = false;
        settings.enabled = false;
        gameplay.enabled = false;
        pause.enabled = false;
        results.enabled = false;
        upgrade.enabled = false;
        win.enabled = true;
    }
}
