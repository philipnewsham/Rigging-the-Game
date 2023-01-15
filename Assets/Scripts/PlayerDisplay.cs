using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField] private Text _numberText;
    [SerializeField] private Image _shirtImage;

    public void SetPlayer(int number, Sprite shirt)
    {
        _numberText.text = number.ToString();
        _shirtImage.sprite = shirt;
    }
}
