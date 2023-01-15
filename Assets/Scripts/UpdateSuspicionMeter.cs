using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSuspicionMeter : MonoBehaviour
{
    [SerializeField] private SuspicionMeter _suspicionMeter;

    void OnEnable()
    {
        MoveTeamButton.OnPlayerAdded += OnPlayerAdded;
        MoveTeamButton.OnPlayerRemoved += OnPlayerRemoved;
    }

    private void OnPlayerAdded(Player player)
    {
        _suspicionMeter.AddPercentage(player.ReturnPlayerWorth());
    }

    private void OnPlayerRemoved(Player player)
    {
        _suspicionMeter.AddPercentage(-player.ReturnPlayerWorth());
    }

    private void OnDisable()
    {
        MoveTeamButton.OnPlayerAdded -= OnPlayerAdded;
        MoveTeamButton.OnPlayerRemoved -= OnPlayerRemoved;
    }
}
