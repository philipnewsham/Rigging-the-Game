using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamNameInputField : MonoBehaviour
{
    [SerializeField] private Button _beginButton;

    public void OnValueChanged()
    {
        int m_textLength = GetComponent<InputField>().text.Length;
        _beginButton.interactable = m_textLength > 0;
    }
}
