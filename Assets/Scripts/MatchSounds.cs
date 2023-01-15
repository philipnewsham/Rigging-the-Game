using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _whistleSFX;
    [SerializeField] private AudioClip _goalScoredSFX;
    [SerializeField] private AudioClip _goalMissedSFX;
    [SerializeField] private AudioClip _ballKickedSFX;

    private void OnEnable()
    {
        SimulateMatch.OnSetInitialPlayer += OnStart;
        SimulateMatch.OnGoalScored += OnGoalScored;
        SimulateMatch.OnGoalMissed += OnGoalMissed;
        SimulateMatch.OnBallPassed += OnBallKicked;
    }

    public void OnStart(Player player)
    {
        AudioController.PlayAudioClip(_whistleSFX);
    }

    public void OnBallKicked(Player playerA, Player playerB, Player playerC, int segmentCount)
    {
        AudioController.PlayAudioClip(_ballKickedSFX);
    }

    public void OnGoalScored(bool isPlayerTeam, int scoreCount)
    {
        AudioController.PlayAudioClip(_goalScoredSFX);
    }

    public void OnGoalMissed()
    {
        AudioController.PlayAudioClip(_goalMissedSFX);
    }

    private void OnDisable()
    {
        SimulateMatch.OnSetInitialPlayer -= OnStart;
        SimulateMatch.OnGoalScored -= OnGoalScored;
        SimulateMatch.OnGoalMissed -= OnGoalMissed;
        SimulateMatch.OnBallPassed -= OnBallKicked;
    }
}
