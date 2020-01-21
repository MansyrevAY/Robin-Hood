using UnityEngine;

/// <summary>
/// This class holds general logic about all units which have HP, take damage and attack enemies
/// </summary>
public abstract class UnitBehaviourBase : MonoBehaviour, IDamagable
{    
    public StatsSO characteristics;
    public int health;              // Only for representation in the Inspector

    protected IDamagable targetDamagable;
    protected GameObject currentTarget;
    protected int maxHealth;
    protected int damage = 20;
    protected float attackSpeed = 0.5f;
    protected int currentHealth;
    protected bool inCombat = false;

    protected void SetBaseStats() // Хочу сделать его обязательным для вызова, но не знаю, как
    {
        damage = characteristics.damage;
        attackSpeed = characteristics.attackSpeed;
        maxHealth = characteristics.maxHP;
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

    protected virtual void DealDamageToTarget() // Очень хочу вынести это в интерфейс, но не хочу делать пабликом, пока висит здесь
    {
        bool condition = false;

        if (currentTarget != null || !currentTarget.activeInHierarchy)
            targetDamagable.TakeDamage(damage, out condition);
        else
            GetNextTarget();

        if (condition)
        {
            inCombat = false;

            GetNextTarget();
        }
    }

    protected abstract void GetNextTarget();
}
