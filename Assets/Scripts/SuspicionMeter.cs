using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionMeter : MonoBehaviour
{
    [SerializeField] private RectTransform _baseRectTransform;
    [SerializeField] private RectTransform _coverRectTransform;
    [SerializeField] private float _slideDuration = 1.0f;
    private float _coverHeight;
    private float _maxWidth;
    private float _currentPercentage = 1.0f;

    void Start()
    {
        _maxWidth = _baseRectTransform.rect.width;
        _coverHeight = _coverRectTransform.sizeDelta.y;
        _coverRectTransform.sizeDelta = new Vector2(_maxWidth, _coverHeight);
    }

    public void AddPercentage(float percentage)
    {
        StartCoroutine(SlideBarToPercent(_currentPercentage, Mathf.Clamp01(_currentPercentage + percentage), _slideDuration));
    }

    public void RemovePercentage(float percentage)
    {
        StartCoroutine(SlideBarToPercent(_currentPercentage, Mathf.Clamp01(_currentPercentage - percentage), _slideDuration));
    }

    IEnumerator SlideBarToPercent(float currentPercent, float targetPercent, float duration)
    {
        float m_t = 0.0f;
        
        while (m_t <= 1.0f)
        {
            float m_percentage = Mathf.Lerp(currentPercent, targetPercent, m_t);
            float m_x = Mathf.Lerp(0.0f, _maxWidth, m_percentage);
            _coverRectTransform.sizeDelta = new Vector2(m_x, _coverHeight);
            m_t += Time.deltaTime / duration;
            yield return null;
        }

        _coverRectTransform.sizeDelta = new Vector2(Mathf.Lerp(0.0f, _maxWidth, targetPercent), _coverHeight);
        _currentPercentage = targetPercent;
    }
}
