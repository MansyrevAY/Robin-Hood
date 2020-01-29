using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public HPSO originalHealth;
    [Tooltip("Current health, representation only")]
    public int health;
    public Image hpBarImage;

    private int maxHealth;
    private int currentHealth;
    private GameObject hpBarObject;

    private void Awake()
    {
        maxHealth = originalHealth.maxHP;
        currentHealth = maxHealth;
        health = currentHealth;

        hpBarObject = hpBarImage.gameObject.transform.parent.gameObject;
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

        UpdateHpBar();
    }

    private void UpdateHpBar()
    {
        if (!hpBarObject.activeInHierarchy)
            hpBarObject.SetActive(true);

        if (hpBarImage != null) // Хотим ли мы показывать хп для всяких препятствий?
            hpBarImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
