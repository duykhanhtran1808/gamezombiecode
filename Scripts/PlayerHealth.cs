using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHitPoints = 100f;
    [SerializeField] TextMeshProUGUI healthText;

    private void Update()
    {
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        healthText.text = "Health: " + playerHitPoints;
    }
    public void TakeDamage(float damage)
    {
        this.playerHitPoints -= damage;
        if (this.playerHitPoints <= 0)
        {
            this.GetComponent<DeathHandler>().HandleDeath();
        }
    }

    public void IncreaseHealth(float health)
    {
        this.playerHitPoints += health;
    }
}
