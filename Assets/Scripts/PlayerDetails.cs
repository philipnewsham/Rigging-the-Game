using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetails : MonoBehaviour
{
    [SerializeField] private Text _playerText;
    [SerializeField] private Text _abilityText;
    [SerializeField] private Text _worksWithOthersText;
    [SerializeField] private Text _energyText;
    [SerializeField] private Text _roleText;
    [SerializeField] private Text _buttonText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _parentGameObject;

    private void OnEnable()
    {
        PlayerButton.OnButtonPressed += OnPlayerButtonSelected;
    }

    void OnPlayerButtonSelected(PlayerButton playerButton)
    {
        WritePlayerInformation(playerButton);
    }

    public void WritePlayerInformation(PlayerButton playerButton)
    {
        Player m_player = playerButton.player;
        _playerText.text = m_player.playerName;
        _abilityText.text = string.Format("Ability: {0}", m_player.ability);
        _worksWithOthersText.text = string.Format("Works with others: {0}", m_player.workingWithOthers);
        _energyText.text = string.Format("Current energy: {0}", m_player.energy);
        _roleText.text = string.Format("Role: {0}", m_player.role);
        _buttonText.text = m_player.isOnMainTeam ? "Remove from main team" : "Add to main team";
        _parentGameObject.SetActive(true);
    }

    public void HidePlayerInformation()
    {
        _parentGameObject.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerButton.OnButtonPressed -= OnPlayerButtonSelected;
    }

}
