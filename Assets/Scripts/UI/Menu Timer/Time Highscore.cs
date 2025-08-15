using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeHighscore : MonoBehaviour
{
    private Timer timer;
    private TMP_Text timerText;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        timerText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        timerText.text = timer.endTime.ToString("F2") + "s";
    }
}