using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : MonoBehaviour, IDamagable
{
    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int amount)
    {
        bool condition = false;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Invoke("StartDeath", 0.1f);
            condition = true;
        }

        return condition;
    }

    private void StartDeath()
    {
        Destroy(gameObject);
    }
}
