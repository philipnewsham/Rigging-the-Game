using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialiseTeamLineup : MonoBehaviour
{
    [SerializeField] private GamePlayLoop _gamePlayLoop;
    [SerializeField] private Transform _mainTeamParent;
    [SerializeField] private Transform _substituteTeamParent;
    [SerializeField] private PlayerButton _playerButtonPrefab;
    [SerializeField] private HorizontalLayoutGroup _mainTeamLayoutGroup;
    [SerializeField] private HorizontalLayoutGroup _subTeamLayoutGroup;

    private void OnEnable()
    {
        StartCoroutine(SetPlayers());
    }

    IEnumerator SetPlayers()
    {
        Team m_playerTeam = _gamePlayLoop.playerTeam;

        for (int i = 0; i < m_playerTeam.mainPlayers.Length; i++)
        {
            Instantiate(_playerButtonPrefab, _mainTeamParent).SetPlayer(m_playerTeam.mainPlayers[i]);
        }

        for (int i = 0; i < m_playerTeam.substitutePlayers.Length; i++)
        {
            Instantiate(_playerButtonPrefab, _substituteTeamParent).SetPlayer(m_playerTeam.substitutePlayers[i]);
        }

        yield return null;
        _mainTeamLayoutGroup.enabled = false;
        _subTeamLayoutGroup.enabled = false;
        yield return null;
        _mainTeamLayoutGroup.enabled = true;
        _subTeamLayoutGroup.enabled = true;
    }
}
