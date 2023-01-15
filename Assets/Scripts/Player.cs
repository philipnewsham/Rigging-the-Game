using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerName;
    public int energy;
    public int ability;
    public int workingWithOthers;
    public bool isOnMainTeam = false;
    public Role role;
    public int number;

    public Player(string playerName, int number, int ability, int workingWithOthers, Role role)
    {
        this.playerName = playerName;
        this.number = number;
        energy = 5;
        this.ability = ability;
        this.workingWithOthers = workingWithOthers;
        this.role = role;
    }

    public float ReturnPlayerWorth()
    {
        int m_totalWorth = energy + (ability * 2) + workingWithOthers;  //get player's points 5 + (5 * 2) + 5 = 20
        float m_t = Mathf.InverseLerp(0, 20, m_totalWorth);             //convert to 0 - 1
        m_t -= 0.5f;                                                    //shift to -0.5f to 0.5f
        return  m_t * 0.5f;                                          //half to -0.25 to 0.25f
    }
}

public enum Role
{
    Attacker,
    Defender,
    AllRounder
}
