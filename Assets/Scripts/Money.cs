using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int _money;

    public void GainMoney(int moneyGained)
    {
        _money = Mathf.Max(0, _money + moneyGained);
    }
}
