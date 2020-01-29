using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public HPSO originalHealth;

    private int maxHealth;
    private int currentHealth;

    [Tooltip("Current health, representation only")]
    public int health;

    private void Awake()
    {
        maxHealth = originalHealth.maxHP;
        currentHealth = maxHealth;
        health = currentHealth;
    }

    public virtual void TakeDamage(int amount, out bool condition) // I don't like this "out bool", but it's my best idea for now
    {
        condition = false;

        currentHealth -= amount;
        health = currentHealth;

        if (currentHealth <= 0)
        {
            condition = true;
            gameObject.SetActive(false);
        }

    }
}
