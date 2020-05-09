using System.Collections;
using UnityEngine;

public class HoodBehaviour : AttackBehaviour
{
    public DistributeAttackers targetDistributor;
    [Tooltip("How often is target positon updated")]
    public float targetUpdateSpeed;

    private MovementBehaviour movement;

    private void Awake()
    {
        movement = GetComponent<MovementBehaviour>();
        SetBaseStats();
    }

    protected override void SetBaseStats()
    {
        damage = originalAttack.damage;
        animationBehaviour = GetComponent<AnimationScript>();
    }

    private void Update()
    {
        if (!targetDistributor.TargetsExist)
        {
            movement.StopMovement();
        }
    }

    public override void Attack(GameObject guard)
    {
        currentTarget = guard;
        movement.Chase(guard);
        targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HealthBehaviour>())
        {
            InCombat = true;
            movement.StopMovement();
            transform.LookAt(other.transform);
        }
    }

    protected override void GetNextTarget()
    {
        if(targetDistributor.TargetsExist)
            Attack(targetDistributor.GetTargetFor(gameObject));
    }
}
