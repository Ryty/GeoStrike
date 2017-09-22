using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Score and combo texts")]
    public Text scoreText;
    [Header("Timers")]
    public Text timerUp;
    public Text timerMid;

    private void Awake()
    {
        scoreText.enabled = false;
        timerUp.enabled = false;
        timerMid.enabled = false;
    }
}
