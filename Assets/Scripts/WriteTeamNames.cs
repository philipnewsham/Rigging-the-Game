using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteTeamNames : MonoBehaviour
{
    public Text playerTeamNameText;
    public Text enemyTeamNameText;

    public void WriteNames(Team playerTeam, Team enemyTeam)
    {
        playerTeamNameText.text = playerTeam.teamName;
        enemyTeamNameText.text = enemyTeam.teamName;
    }
}
