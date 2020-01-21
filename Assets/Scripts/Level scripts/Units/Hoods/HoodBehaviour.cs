using UnityEngine;

[RequireComponent(typeof(MoveToLocation))]
public class HoodBehaviour : UnitBehaviourBase, IAttacking
{
    
    public DistributeAttackers targetDistributor;

    private MoveToLocation hoodMovement;
    private float lastAttacktime = 0;

    private void Awake()
    {
        hoodMovement = GetComponent<MoveToLocation>();
        SetBaseStats();
    }

    private void Update()
    {
        if (inCombat && Time.time - lastAttacktime > attackSpeed)
        {
            DealDamageToTarget();
            lastAttacktime = Time.time;
        }
    }

    public void Attack(GameObject guard)
    {
        currentTarget = guard;
        hoodMovement.ChangeDestination(guard.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targetDamagable = currentTarget.GetComponent<IDamagable>();
            inCombat = true;
        }
    }

    protected override void GetNextTarget()
    {
        if (targetDistributor.TargetsExist)
            Attack(targetDistributor.GetTargetFor(gameObject));
    }
}
