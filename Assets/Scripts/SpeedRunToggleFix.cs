using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedRunToggleFix : MonoBehaviour
{
    GameController gameController;
    Toggle toggle;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        toggle = GetComponent<Toggle>();
        StartCoroutine(FixSpeedRunToggle());
    }

    IEnumerator FixSpeedRunToggle()
    {
        yield return new WaitForEndOfFrame();
        if (gameController.gameType == GameType.SpeedRun)
            toggle.isOn = true;
        else
            toggle.isOn = false;

        toggle.onValueChanged.AddListener((value) => gameController.ToggleSpeedRun(toggle.isOn));
    }
}
