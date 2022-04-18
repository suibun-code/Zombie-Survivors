using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static int Money { get; set; } = 100;

    public static void AddMoney(int money)
    {
        Money += money;
        IngameUI.instance.SetMoneyText();
    }

    public static bool SubtractMoney(int money)
    {
        if (Money - money < 0)
        {
            Debug.Log("NO MONEY TO BUILD OBSTACLE");
            return false;
        }

        Money -= money;
        IngameUI.instance.SetMoneyText();

        return true;
    }
}
