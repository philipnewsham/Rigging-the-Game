using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimulateMatch : MonoBehaviour
{
    [SerializeField] private GamePlayLoop _gamePlayLoop;
    [SerializeField] private int _sideSegments = 3;
    [SerializeField] private float _minTimeBetweenAction = 4.0f;
    [SerializeField] private float _maxTimeBetweenAction = 8.0f;
    [SerializeField] private float _delayAfterGoal = 1.0f;
    [SerializeField] private float _shootingMultiplier = 3.0f;
    [SerializeField] private float _savingMultiplier = 1.0f;

    private int _currentSegment = 0;
    private bool _isPlayerTeam = true;
    private Player _currentPlayer;
    private int _playerTeamScore = 0;
    private int _enemyTeamScore = 0;

    public static event Action<int> OnBallMoved;
    public static event Action<bool, int> OnGoalScored;
    public static event Action<Player, Player, Player, int> OnBallPassed;
    public static event Action<Player> OnSetInitialPlayer;
    public static event Action OnGoalMissed;

    public IEnumerator Simulate(bool doesPlayerTeamStart)
    {
        _isPlayerTeam = doesPlayerTeamStart;
        SetInitialPlayer();

        while (true) 
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(_minTimeBetweenAction, _maxTimeBetweenAction));

            if (!IsInScoringRange())
            {
                TryPassForward(_currentPlayer, ReturnTargetPlayer(_isPlayerTeam, _currentPlayer), ReturnTargetPlayer(!_isPlayerTeam, null));
                continue;
            }

            if (ShootAtGoal(_currentPlayer, ReturnTargetPlayer(!_isPlayerTeam, null)))
            {
                ScoreGoal();
                yield return new WaitForSeconds(_delayAfterGoal);
                continue;
            }

            MissGoal();
        }
    }

    void SetInitialPlayer()
    {
        _currentPlayer = ReturnTargetPlayer(_isPlayerTeam, null);
        OnSetInitialPlayer?.Invoke(_currentPlayer);
    }

    bool IsInScoringRange()
    {
        return Mathf.Abs(_currentSegment) == _sideSegments;
    }

    Player ReturnTargetPlayer(bool onPlayerTeam, Player ignorePlayer)
    {
        if (onPlayerTeam)
        {
            return _gamePlayLoop.playerTeam.ReturnFreePlayer(ignorePlayer);
        }

        return _gamePlayLoop.enemyTeam.ReturnFreePlayer(ignorePlayer);
    }

    void TryPassForward(Player playerPassingFrom, Player playerPassingTo, Player playerIntercepting)
    {
        if (PassComplete())
        {
            PassToPlayer(playerPassingFrom, playerPassingTo);
            OnBallPassed?.Invoke(playerPassingFrom, playerPassingTo, playerIntercepting, _currentSegment);
            return;
        }

        InterceptPass(playerIntercepting);
    }

    void PassToPlayer(Player playerPassingFrom, Player playerPassingTo)
    {
        _currentPlayer = playerPassingTo;
        Debug.LogFormat("{0} passes to {1}", playerPassingFrom.playerName, playerPassingTo.playerName);
        MoveBetweenSegments(_isPlayerTeam);
    }

    void InterceptPass(Player playerIntercepting)
    {
        Debug.LogFormat("{0} intercepted!", playerIntercepting.playerName);
        _isPlayerTeam = !_isPlayerTeam;
        _currentPlayer = playerIntercepting;
    }

    void MoveBetweenSegments(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            _currentSegment++;
        }
        else
        {
            _currentSegment--;
        }

        OnBallMoved?.Invoke(_currentSegment);
    }

    bool PassComplete()
    {
        return true;
    }

    bool ShootAtGoal(Player playerScoring, Player playerSaving)
    {
        float m_chanceToScore = ReturnMultiplierFromRole(playerScoring, Role.Attacker) * _shootingMultiplier * (playerScoring.ability + playerScoring.energy);
        float m_defendingAmount = ReturnMultiplierFromRole(playerSaving, Role.Defender) * _savingMultiplier * (playerSaving.ability + playerScoring.energy);
        return UnityEngine.Random.Range(0, 100) <= (m_chanceToScore - m_defendingAmount);
    }

    void ScoreGoal()
    {
        Debug.LogFormat("{0} scored for {1} team!", _currentPlayer.playerName, _isPlayerTeam ? "Your" : "Enemy");
        IncreaseScore(_isPlayerTeam);
        OnGoalScored?.Invoke(_isPlayerTeam, ReturnScore(_isPlayerTeam));
        _currentSegment = 0;
        _isPlayerTeam = !_isPlayerTeam;
        _currentPlayer = ReturnTargetPlayer(_isPlayerTeam, null);
        OnBallMoved?.Invoke(_currentSegment);
    }

    void MissGoal()
    {
        Debug.LogFormat("{0} missed the goal!", _currentPlayer);
        _isPlayerTeam = !_isPlayerTeam;
        MoveBetweenSegments(_isPlayerTeam);
        _currentPlayer = ReturnTargetPlayer(_isPlayerTeam, null);
        OnGoalMissed?.Invoke();
    }

    void IncreaseScore(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            _playerTeamScore++;
            return;
        }

        _enemyTeamScore++;
    }

    public int ReturnScore(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            return _playerTeamScore;
        }

        return _enemyTeamScore;
    }

    float ReturnMultiplierFromRole(Player player, Role targetRole)
    {
        if(player.role == targetRole)
            return 2.0f;
        if (player.role == Role.AllRounder)
            return 1.5f;

        return 1.0f;
    }
}
