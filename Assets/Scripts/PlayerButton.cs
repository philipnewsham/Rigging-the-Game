using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerButton : MonoBehaviour
{
    public Player player;
    public Text _nameText;
    public static event Action<PlayerButton> OnButtonPressed;
    public bool isHighlighted;
    [SerializeField] private Button _button;
    [SerializeField] private Color _highlightColor;
    [SerializeField] private StatusBar _energyStatusBar;
    [SerializeField] private StatusBar _abilityStatusBar;
    [SerializeField] private StatusBar _workingWithOthersStatusBar;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => PressedButton());    
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        _nameText.text = player.playerName;
        _energyStatusBar.SetBarWidthFromPercent((float)player.energy / 5.0f);
        _abilityStatusBar.SetBarWidthFromPercent((float)player.ability / 5.0f);
        _workingWithOthersStatusBar.SetBarWidthFromPercent((float)player.workingWithOthers / 5.0f);
    }

    void PressedButton()
    {
        OnButtonPressed?.Invoke(this);
    }

    public void SetHighlight()
    {
        _button.image.color = _highlightColor;
        isHighlighted = true;
    }

    public void UnSetHighlight()
    {
        _button.image.color = Color.white;
        isHighlighted = false;
    }
}
