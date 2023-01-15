using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoveTeamButton : MonoBehaviour
{
    [SerializeField] private Button _moveButton;
    [SerializeField] private Transform _mainTeamParent;
    [SerializeField] private Transform _substituteTeamParent;
    [SerializeField] private GamePlayLoop _gamePlayLoop;
    private PlayerButton _currentSelectedPlayerButton;

    public static event Action<Player> OnPlayerAdded;
    public static event Action<Player> OnPlayerRemoved;

    private void OnEnable()
    {
        PlayerButton.OnButtonPressed += OnButtonPressed;
    }

    private void OnButtonPressed(PlayerButton playerButton)
    {
        if (!playerButton.isHighlighted)
        {
            UnSelectButton();
            playerButton.SetHighlight();
            _currentSelectedPlayerButton = playerButton;
            return;
        }

        Player m_player = playerButton.player;
        _moveButton.onClick.RemoveAllListeners();

        UnSelectButton();
        if (m_player.isOnMainTeam)
        {
            RemovePlayer(playerButton);
            return;
        }

        AddPlayer(playerButton);
    }

    void UnSelectButton()
    {
        if(_currentSelectedPlayerButton == null)
        {
            return;
        }

        _currentSelectedPlayerButton.UnSetHighlight();
        _currentSelectedPlayerButton = null;
    }

    void RemovePlayer(PlayerButton playerButton)
    {
        playerButton.transform.parent = _substituteTeamParent;
        _gamePlayLoop.playerTeam.RemovePlayer(playerButton.player);
        OnPlayerRemoved?.Invoke(playerButton.player);
    }

    void AddPlayer(PlayerButton playerButton)
    {
        playerButton.transform.parent = _mainTeamParent;
        _gamePlayLoop.playerTeam.AddPlayer(playerButton.player);
        OnPlayerAdded?.Invoke(playerButton.player);
    }

    private void OnDisable()
    {
        PlayerButton.OnButtonPressed -= OnButtonPressed;
    }
}
