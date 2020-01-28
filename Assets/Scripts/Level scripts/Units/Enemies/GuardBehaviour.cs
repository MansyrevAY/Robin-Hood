using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehaviour : AttackBehaviour
{
    private Queue<GameObject> attackers = new Queue<GameObject>();

    void Awake() => SetBaseStats();

    private void Update()
    {
        MakeAttack();
    }

    private void OnTriggerEnter(Collider other) => Attack(other.gameObject);

    public override void Attack(GameObject hood)
    {
        if (hood.tag != "Hood")
            return;

        attackers.Enqueue(hood);

        if (currentTarget == null)
        {
            currentTarget = attackers.Dequeue();
            transform.LookAt(currentTarget.transform); // надо переделать в более плавный поворот
            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
        }

        InCombat = true;
    }

    protected override void GetNextTarget()
    {
        if (attackers.Count > 0)
            currentTarget = attackers.Dequeue();
    }

    protected override void SetBaseStats()
    {
        damage = originalAttack.damage;
        attackSpeed = originalAttack.attackSpeed;
    }
}
