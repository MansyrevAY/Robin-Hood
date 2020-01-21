using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : UnitBehaviourBase, IAttacking
{
    private Queue<GameObject> attackers = new Queue<GameObject>();

    void Awake() => SetBaseStats();

    void Start()
    {
        currentHealth = maxHealth;
        health = maxHealth;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) => Attack(other.gameObject);

    public void Attack(GameObject hood)
    {
        attackers.Enqueue(hood);

        if (currentTarget == null)
            currentTarget = attackers.Dequeue();

        inCombat = true;
    }

    protected override void GetNextTarget()
    {
        if (attackers.Count > 0)
            currentTarget = attackers.Dequeue();
    }
}
