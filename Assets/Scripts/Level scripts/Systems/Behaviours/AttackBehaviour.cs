using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public AttackSO originalAttack;

    protected bool inCombat = false;
    protected HealthBehaviour targetDamagable;
    protected GameObject currentTarget;
    protected int damage = 20;
    protected float attackSpeed = 0.5f;
    protected float lastAttacktime = 0;

    /// <summary>
    /// Starts to perform attacking actions for the target
    /// </summary>
    /// <param name="target"></param>
    public abstract void Attack(GameObject target);

    /// <summary>
    /// Calls DealDamage every attackSpeed time if inCombat is true
    /// </summary>
    protected virtual void MakeAttack()
    {
        if (inCombat && Time.time - lastAttacktime > attackSpeed)
        {
            if (currentTarget == null || targetDamagable == null)
                GetNextTarget();
            else
            {
                DealDamageToTarget();
                lastAttacktime = Time.time;
            }
        }
    }

    /// <summary>
    /// Deals damage if can, if not, calls GetNextTarget
    /// </summary>
    protected virtual void DealDamageToTarget()
    {
        bool condition = false;

        if (currentTarget != null)
        {
            if (currentTarget.activeInHierarchy)
                targetDamagable.TakeDamage(damage, out condition);
        }

        else
            GetNextTarget();

        if (condition)
        {
            inCombat = false;

            GetNextTarget();
        }
    }

    /// <summary>
    /// Switches target according to individual algorythms
    /// </summary>
    protected abstract void GetNextTarget();
    
    /// <summary>
    /// Sets up stats got from Scriptable Object
    /// </summary>
    protected abstract void SetBaseStats();
}
