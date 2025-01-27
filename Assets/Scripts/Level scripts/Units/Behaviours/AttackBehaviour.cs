﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationScript))]
[RequireComponent(typeof(MovementBehaviour))]
public abstract class AttackBehaviour : MonoBehaviour
{
    public AttackSO originalAttack;

    public bool InCombat { get; protected set; }

    protected HealthBehaviour targetDamagable;
    protected GameObject currentTarget;
    protected int damage = 20;
    protected float lastAttacktime = 0;
    protected AnimationScript animationBehaviour;

    private float _attackSpeed = 0f;
    public float AttackSpeed { get => _attackSpeed == 0f ? _attackSpeed : originalAttack.attackSpeed; }    

    protected enum TargetCondition { TargetAlive, TargetKilled };

    /// <summary>
    /// Starts to perform attacking actions for the target
    /// </summary>
    /// <param name="target"></param>
    public abstract void Attack(GameObject target);

    public virtual void MakeAttack()
    {
        if (currentTarget == null || targetDamagable == null || !currentTarget.activeInHierarchy)
        {
            InCombat = false;
            GetNextTarget();
        }

        else
        {
            if (DealDamageToTarget() == TargetCondition.TargetKilled)
            {
                InCombat = false;
                currentTarget = null;
                GetNextTarget();
            }
        }

    }

    /// <summary>
    /// Deals damage if can, returns target condition
    /// </summary>
    protected virtual TargetCondition DealDamageToTarget()
    {
        bool isDead = false;

        if (currentTarget == null)
            return TargetCondition.TargetKilled;

        if (currentTarget.activeInHierarchy)
        {
            targetDamagable.TakeDamage(damage, out isDead);
        }

        if (isDead)
            return TargetCondition.TargetKilled;

        return TargetCondition.TargetAlive;
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
