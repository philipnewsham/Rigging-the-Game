using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    public Player[] mainPlayers;
    public Player[] substitutePlayers;
    private Player _holdPlayer;
    public string teamName;

    public Team(Player[] players, int mainPlayerCount, int subPlayerCount, string teamName)
    {
        this.teamName = teamName;
        mainPlayers = new Player[mainPlayerCount];
        for (int i = 0; i < mainPlayerCount; i++)
        {
            players[i].isOnMainTeam = true;
            mainPlayers[i] = players[i];
        }

        substitutePlayers = new Player[subPlayerCount];
        for (int i = 0; i < subPlayerCount; i++)
        {
            players[i + mainPlayerCount].isOnMainTeam = false;
            substitutePlayers[i] = players[i + mainPlayerCount];
        }
    }

    public Player ReturnFreePlayer(Player ignorePlayer)
    {
        while (true)
        {
            int index = Random.Range(0, mainPlayers.Length);
            if (mainPlayers[index] != ignorePlayer)
            {
                return mainPlayers[index];
            }
        }
    }

    public void RemovePlayer(Player player)
    {
        player.isOnMainTeam = false;
    }

    public void AddPlayer(Player player)
    {
        player.isOnMainTeam = true;
    }

    public void UpdateTeams()
    {
        Player[] m_allPlayers = new Player[mainPlayers.Length + substitutePlayers.Length];

        for (int i = 0; i < mainPlayers.Length; i++)
        {
            m_allPlayers[i] = mainPlayers[i];
        }

        for (int i = 0; i < substitutePlayers.Length; i++)
        {
            m_allPlayers[mainPlayers.Length + i] = substitutePlayers[i];
        }

        int m_mainIndex = 0;
        int m_subIndex = 0;

        for (int i = 0; i < m_allPlayers.Length; i++)
        {
            if (m_allPlayers[i].isOnMainTeam)
            {
                mainPlayers[m_mainIndex] = m_allPlayers[i];
                m_mainIndex++;
                continue;
            }

            substitutePlayers[m_subIndex] = m_allPlayers[i];
            m_subIndex++;
        }
    }
}
