using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zombie : MonoBehaviour
{
    public int health = 100;

    public TextMeshProUGUI healthText;

    public void SubtractHealth(int health)
    {
        if (this.health - health <= 0)
        {
            PlayerStats.AddScore(1);
            PlayerStats.AddMoney(15);
            Destroy(gameObject);
        }
        else
        {
            this.health -= health;
            healthText.text = this.health.ToString();
        }
    }
}
