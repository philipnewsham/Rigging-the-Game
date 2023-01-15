using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamScoreText : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private bool _isPlayerTeam;
    
    private void OnEnable()
    {
        SimulateMatch.OnGoalScored += OnGoalScored;
    }

    private void OnGoalScored(bool isPlayerTeam, int score)
    {
        if(isPlayerTeam != _isPlayerTeam)
        {
            return;
        }

        _scoreText.text = score.ToString();
    }

    private void OnDisable()
    {
        SimulateMatch.OnGoalScored -= OnGoalScored;
    }

}
