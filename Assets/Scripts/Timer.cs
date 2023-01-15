using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    public event Action OnTimerFinished;

    public void StartTimer(float startValue, float targetValue, float durationInSeconds)
    {
        StartCoroutine(TimerCoroutine(startValue, targetValue, durationInSeconds));
    }

    IEnumerator TimerCoroutine(float startValue, float targetValue, float durationInSeconds)
    {
        float t = 0.0f;
        while (t <= 1.0f)
        {
            WriteTime(Mathf.Lerp(startValue, targetValue, t));
            t += Time.deltaTime / durationInSeconds;
            yield return null;
        }
        WriteTime(targetValue);
        OnTimerFinished?.Invoke();
    }

    void WriteTime(float currentTime)
    {
        float minute = Mathf.Floor(currentTime);
        float second = Mathf.Floor((currentTime - minute) * 100) / 100.0f * 60.0f;
        _timerText.text = string.Format("{0}:{1}", minute.ToString("00"), second.ToString("00"));
    }
}
