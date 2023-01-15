using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private RectTransform _parentBarRectTransform;
    [SerializeField] private RectTransform _childBarRectTransform;
    private float _maxWidth;
    private float _height;

    private void Awake()
    {
        _maxWidth = _parentBarRectTransform.rect.width;
        _height = _parentBarRectTransform.rect.height;
    }

    public void SetBarWidthFromPercent(float percentFilled)
    {
        _childBarRectTransform.sizeDelta = new Vector2(Mathf.Lerp(0.0f, _maxWidth, percentFilled), _height);
    }
}
