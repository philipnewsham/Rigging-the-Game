using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMatch : MonoBehaviour
{
    [SerializeField] private RectTransform _footballRectTransform;
    [SerializeField] private Dictionary<Player, PlayerDisplay> _playerDictionary = new Dictionary<Player, PlayerDisplay>();
    [SerializeField] private GamePlayLoop _gamePlayLoop;
    [SerializeField] private PlayerDisplay _playerPrefab;
    [SerializeField] private Sprite _redShirt;
    [SerializeField] private Sprite _blueShirt;
    [SerializeField] private Transform _playerParent;
    private Vector2[] _startingPositions = new Vector2[10] 
    { 
        new Vector2(0.2f, 0.7f),
        new Vector2(0.2f, 0.3f),
        new Vector2(0.3f, 0.7f),
        new Vector2(0.3f, 0.3f),
        new Vector2(0.4f, 0.5f),
        new Vector2(0.8f, 0.7f),
        new Vector2(0.8f, 0.3f),
        new Vector2(0.7f, 0.7f),
        new Vector2(0.7f, 0.3f),
        new Vector2(0.6f, 0.5f),

    };

    private void OnEnable()
    {
        SimulateMatch.OnBallMoved += OnBallMoved;
        SimulateMatch.OnBallPassed += OnBallPassed;
        SimulateMatch.OnGoalScored += OnGoalScored;
        SimulateMatch.OnSetInitialPlayer += OnSetInitialPlayer;
    }

    public void GeneratePlayerDictionary()
    {
        _playerDictionary.Clear();
        
        for (int i = 0; i < _gamePlayLoop.playerTeam.mainPlayers.Length; i++)
        {
            PlayerDisplay m_player = Instantiate(_playerPrefab, _playerParent);
            m_player.SetPlayer(i + 1, _redShirt);
            SetAnchors(m_player.rectTransform, _startingPositions[i]);
            _playerDictionary.Add(_gamePlayLoop.playerTeam.mainPlayers[i], m_player);
        }

        for (int i = 0; i < _gamePlayLoop.enemyTeam.mainPlayers.Length; i++)
        {
            PlayerDisplay m_player = Instantiate(_playerPrefab, _playerParent);
            m_player.SetPlayer(i + 1, _blueShirt);
            SetAnchors(m_player.rectTransform, _startingPositions[i+5]);
            _playerDictionary.Add(_gamePlayLoop.enemyTeam.mainPlayers[i], m_player);
        }
    }

    private void OnSetInitialPlayer(Player player)
    {
        _playerDictionary[player].rectTransform.anchoredPosition = Vector3.zero;
        //StartCoroutine(MoveToPosition(, Vector3.zero, 0.5f));
    }

    private void OnBallReset(Player player)
    {
        StartCoroutine(MoveToPosition(_playerDictionary[player].rectTransform, Vector3.zero, 0.5f));
    }

    private void OnBallMoved(int currentSegment)
    {
        //StartCoroutine(MoveToPosition(_footballRectTransform, new Vector2(ReturnXPosition(currentSegment), 0.0f), 1.0f));
    }

    private void OnGoalScored(bool isTeam, int score)
    {
        StartCoroutine(MoveToPosition(_footballRectTransform, new Vector2(ReturnXPosition(isTeam ? 4 : -4), 0.5f), 1.0f));
    }

    private void OnBallPassed(Player playerA, Player playerB, Player playerC, int currentSegment)
    {
        //playerA should already be in position
        Vector2 targetPosition = new Vector2(ReturnXPosition(currentSegment), ReturnYAnchor());
        StartCoroutine(MoveToPosition(_playerDictionary[playerB].rectTransform, targetPosition, 0.5f));
        StartCoroutine(MoveToPosition(_footballRectTransform, targetPosition, 1.0f));
    }

    float ReturnXPosition(int currentSegment)
    {
        return ReturnXAnchor(currentSegment);
        //return currentSegment * 100.0f;
    }

    float ReturnXAnchor(int currentSegment)
    {
        return 0.5f + (currentSegment * 0.1f);
    }

    float ReturnYAnchor()
    {
        return Random.Range(0.2f, 0.8f);
    }
    /*
    IEnumerator MoveToPosition(RectTransform targetRectTransform, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = targetRectTransform.anchoredPosition;
        float t = 0.0f;
        while (t <= 1.0f)
        {
            targetRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        targetRectTransform.anchoredPosition = targetPosition;
    }
    */
    IEnumerator MoveToPosition(RectTransform targetRectTransform, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = new Vector2(targetRectTransform.anchorMin.x, targetRectTransform.anchorMin.y);
        float t = 0.0f;
        while (t <= 1.0f)
        {
            SetAnchors(targetRectTransform, Vector2.Lerp(startPosition, targetPosition, t));
            t += Time.deltaTime / duration;
            yield return null;
        }

        //targetRectTransform.anchoredPosition = targetPosition;
        SetAnchors(targetRectTransform, targetPosition);
    }

    void SetAnchors(RectTransform targetRectTransform, Vector2 anchor)
    {
        targetRectTransform.anchorMin = anchor;
        targetRectTransform.anchorMax = anchor;
        targetRectTransform.anchoredPosition = Vector2.zero;
    }

    private void OnDisable()
    {
        SimulateMatch.OnBallMoved -= OnBallMoved;
        SimulateMatch.OnBallPassed -= OnBallPassed;
        SimulateMatch.OnGoalScored -= OnGoalScored;
    }
}
