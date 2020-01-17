using UnityEngine;

public class GuardBehaviour : MonoBehaviour, IDamagable
{
    public int maxHealth = 100;
    public int health;              // Only for representation in the Inspector

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        health = maxHealth;
    }

    // I don't like this "out bool", but it's my best idea for now
    public void TakeDamage(int amount, out bool condition)
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

    /// <summary>
    /// Зачем, если под это есть отдельный скрипт?
    /// </summary>
    private void StartDeath()
    {
        Destroy(gameObject);
    }
}
