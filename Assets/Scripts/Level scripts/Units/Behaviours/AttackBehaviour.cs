using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationScript))]
public abstract class AttackBehaviour : MonoBehaviour
{
    public AttackSO originalAttack;

    public bool InCombat { get; protected set; }

    protected HealthBehaviour targetDamagable;
    protected GameObject currentTarget;
    protected int damage = 20;
    protected float attackDuration = 0.5f;
    protected float lastAttacktime = 0;
    protected AnimationScript animationBehaviour;

    protected enum TargetCondition { TargetAlive, TargetKilled};

    /// <summary>
    /// Starts to perform attacking actions for the target
    /// </summary>
    /// <param name="target"></param>
    public abstract void Attack(GameObject target);

    /// <summary>
    /// Calls DealDamage every attackSpeed time if inCombat is true
    /// </summary>
    //public virtual void MakeAttack()
    //{
    //    if (InCombat && Time.time - lastAttacktime > attackDuration)
    //    {
    //        if (currentTarget == null || targetDamagable == null)
    //            GetNextTarget();
    //        else
    //        {
    //            lastAttacktime = Time.time;

    //            if (DealDamageToTarget() == TargetCondition.TargetKilled)
    //            {
    //                GetNextTarget();
    //                InCombat = false;
    //            }
    //        }
    //    }
    //}

    public virtual void MakeAttack()
    {
        if (InCombat && Time.time - lastAttacktime > attackDuration)
        {
            if (currentTarget == null || targetDamagable == null)
                GetNextTarget();
            else
            {
                lastAttacktime = Time.time;
                attackDuration = animationBehaviour.GetNextAttackDuration();
                animationBehaviour.PlayNextAttackAnimation();

                Invoke("Swing", attackDuration);                
            }
        }
    }

    protected void Swing()
    {
        if (DealDamageToTarget() == TargetCondition.TargetKilled)
        {
            GetNextTarget();
            InCombat = false;
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
