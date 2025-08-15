using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float startTime = 0f;
    public float endTime;
    private TMP_Text timerText;
    private Endlevel endLevel;
    private bool timerStopped = false;

    private void Start()
    {
        timerText = GetComponent<TMP_Text>();
        endLevel = FindObjectOfType<Endlevel>();
    }

    private void Update()
    {
        if (endLevel != null && endLevel.gameComplete)
        {
            // Stop the timer when the level is complete
            endTime = startTime;
            timerStopped = true;
            Debug.Log(endTime);
        }

        if (!timerStopped)
        {
            startTime += Time.deltaTime;
        }

        // Display timer with two decimals
        if (timerText != null)
        {
            float displayTime = Mathf.Floor(startTime * 100) / 100f;
            timerText.text = displayTime.ToString("F2") + "s";
        }
    }
}