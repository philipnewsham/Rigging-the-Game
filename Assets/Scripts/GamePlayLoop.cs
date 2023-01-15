using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayLoop : MonoBehaviour
{
    public Team playerTeam;
    public Team enemyTeam;
    [SerializeField] private Timer _timer;
    [SerializeField] private bool _eventCompleted;
    [SerializeField] private SimulateMatch _simulateMatch;
    [SerializeField] private GameObject _simulationObject;
    [SerializeField] private GameObject _intermissionObject;
    [SerializeField] private float _halfMatchDuration = 60.0f;
    [SerializeField] private Button _beginButton;
    [SerializeField] private InputField _teamNameInputField;
    private bool _hasBegun = false;
    [SerializeField] private GameObject _begginingObject;
    [SerializeField] private DisplayMatch _displayMatch;
    [SerializeField] private WriteTeamNames _writeTeamNames;
    [SerializeField] private GameObject _matchCompleteObject;
    [SerializeField] private MatchCompleteScreen _matchCompleteScreen;

    private void Awake()
    {
        _beginButton.onClick.AddListener(() => _hasBegun = true);
    }

    void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        _begginingObject.SetActive(true);
        yield return new WaitUntil(() => _hasBegun);
        _begginingObject.SetActive(false);
        LoadData();
        _displayMatch.GeneratePlayerDictionary();

        while (true)
        {
            yield return StartCoroutine(FirstHalf());
            yield return StartCoroutine(Intermission());
            yield return StartCoroutine(SecondHalf());
            yield return StartCoroutine(MatchEnd());
        }
    }

    private void LoadData()
    {
        playerTeam = new Team(GeneratePlayers(10, "Your team:"), 5, 5, _teamNameInputField.text);
        enemyTeam = new Team(GeneratePlayers(10, "Enemy team:"), 5, 5, "Sneaky Snakes");
    }

    Player[] GeneratePlayers(int playerCount, string team)
    {
        Player[] players = new Player[playerCount];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = new Player(string.Format("{0} Player {1}", team, i), i, Random.Range(0, 6), Random.Range(0, 6), (Role)Random.Range(0, 3));
        }
        return players;
    }

    IEnumerator FirstHalf()
    {
        _simulationObject.SetActive(true);
        _writeTeamNames.WriteNames(playerTeam, enemyTeam);
        yield return StartCoroutine(PlayMatchHalf(0.0f, 45.0f, true));
        _simulationObject.SetActive(false);
    }

    public void OnTimerFinished()
    {
        _eventCompleted = true;
    }

    IEnumerator Intermission()
    {
        _intermissionObject.SetActive(true);
        yield return new WaitUntil(()=> _eventCompleted);
        _eventCompleted = false;
        _intermissionObject.SetActive(false);
    }

    public void CompleteIntermission()
    {
        playerTeam.UpdateTeams();
        _eventCompleted = true;
    }

    IEnumerator SecondHalf()
    {
        _simulationObject.SetActive(true);
        yield return StartCoroutine(PlayMatchHalf(45.0f, 90.0f, false));
        _simulationObject.SetActive(false);
    }

    IEnumerator PlayMatchHalf(float startTime, float endTime, bool doesPlayerTeamStart)
    {
        _timer.StartTimer(startTime, endTime, _halfMatchDuration);
        Coroutine m_simulateMatch = StartCoroutine(_simulateMatch.Simulate(doesPlayerTeamStart));
        _timer.OnTimerFinished += OnTimerFinished;
        yield return new WaitUntil(() => _eventCompleted);
        StopCoroutine(m_simulateMatch);
        _timer.OnTimerFinished -= OnTimerFinished;
        _eventCompleted = false;
    }

    IEnumerator MatchEnd()
    {
        _matchCompleteObject.SetActive(true);
        _matchCompleteScreen.WriteMatchComplete(playerTeam.teamName, enemyTeam.teamName, _simulateMatch.ReturnScore(true), _simulateMatch.ReturnScore(false));
        yield return new WaitUntil(() => _eventCompleted);
    }
}
