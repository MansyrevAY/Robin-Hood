using UnityEngine;

[RequireComponent(typeof(MovementBehaviour))]
public class HoodBehaviour : AttackBehaviour
{
    public DistributeAttackers targetDistributor;

    private MovementBehaviour movement;

    private void Awake()
    {
        movement = GetComponent<MovementBehaviour>();
        SetBaseStats();
    }

    protected override void SetBaseStats()
    {
        damage = originalAttack.damage;
        attackSpeed = originalAttack.attackSpeed;
    }

    private void Update()
    {
        MakeAttack();
    }

    public override void Attack(GameObject guard)
    {
        currentTarget = guard;
        movement.ChangeDestination(guard.transform.position);
        targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            InCombat = true;
            movement.StopMovement();
            transform.LookAt(other.transform);
        }
    }

    protected override void GetNextTarget()
    {
        if (targetDistributor.TargetsExist)
            Attack(targetDistributor.GetTargetFor(gameObject));
    }
}
