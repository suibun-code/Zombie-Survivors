using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : Singleton<IngameUI>
{
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText.text = "Money: " + PlayerStats.Money.ToString();
    }

    public void SetMoneyText()
    {
        moneyText.text = "Money: " + PlayerStats.Money.ToString();
    }
}
