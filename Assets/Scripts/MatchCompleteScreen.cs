using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchCompleteScreen : MonoBehaviour
{
    [SerializeField] private Text _matchDescriptionText;
    [SerializeField] private Text _claimWinningsButtonText;
    [SerializeField] private Button _claimWinningsButton;
    [SerializeField] private Money _money;

    public void WriteMatchComplete(string teamAName, string teamBName, int teamAScore, int teamBScore)
    {
        _matchDescriptionText.text = string.Format("Match results:\n{0} vs {1}\n{2} : {3}", teamAName, teamBName, teamAScore, teamBScore);
        int winnings = Mathf.Max(teamBScore - teamAScore, 0) * 100;
        _claimWinningsButtonText.text = string.Format("Claim Winnings\n£{0}", winnings);
        _claimWinningsButton.onClick.RemoveAllListeners();
        _claimWinningsButton.onClick.AddListener(() => _money.GainMoney(winnings));
    }
}
