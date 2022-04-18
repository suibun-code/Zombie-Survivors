using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : Singleton<IngameUI>
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        moneyText.text = "Money: " + PlayerStats.Money.ToString();
        moneyText.text = "Score: " + PlayerStats.Money.ToString();
    }

    public void SetMoneyText()
    {
        moneyText.text = "Money: " + PlayerStats.Money.ToString();
    }

    public void SetScoreText()
    {
        moneyText.text = "Score: " + PlayerStats.Score.ToString();
    }
}
