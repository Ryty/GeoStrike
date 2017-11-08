using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Score and combo texts")]
    public Text scoreText;
    public Text comboText;
    [Header("Timers")]
    public Text timerUp;
    public Text timerMid;
    public Text timerCombo;

    private Timer comboTimer;

    private void Awake()
    {
        comboText.enabled = false;
        scoreText.enabled = false;
        timerUp.enabled = false;
        timerMid.enabled = false;
        timerCombo.enabled = false;

        SetScoreText(0);
    }

    private void Update()
    {
        timerCombo.text = comboTimer.time.ToString("f0");
    }

    public void SetScoreText(int amount)
    {
        scoreText.text = amount.ToString();
    }

    public void SetComboText(float amount)
    {
        comboText.text = amount.ToString("F1") + "X";
    }

    public void SetComboTimer(Timer timer)
    {
        comboTimer = timer;
    }
}
