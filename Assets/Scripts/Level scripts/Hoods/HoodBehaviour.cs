using UnityEngine;

[RequireComponent(typeof(MoveToLocation))]
public class HoodBehaviour : MonoBehaviour
{
    public int damage = 20;
    public float attackSpeed = 0.5f;
    public DistributeAttackers targetDistributor;

    private MoveToLocation hoodMovement;
    private GameObject attackTarget;
    private IDamagable targetDamagable;
    private bool inCombat = false;

    private float lastAttacktime = 0;

    void Awake()
    {
        hoodMovement = GetComponent<MoveToLocation>();
    }

    private void Update()
    {
        if (inCombat && Time.time - lastAttacktime > attackSpeed)
        {
            HitAttackTarget();
            lastAttacktime = Time.time;
        }
    }

    public void Attack(GameObject guard)
    {
        attackTarget = guard;
        hoodMovement.ChangeDestination(guard.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            targetDamagable = attackTarget.GetComponent<IDamagable>();
            inCombat = true;
        }
    }

    private void HitAttackTarget()
    {
        bool condition = false;

        if (attackTarget != null || !attackTarget.activeInHierarchy)
            targetDamagable.TakeDamage(damage, out condition);
        else
            GetNextTarget();

        if (condition)
        {
            inCombat = false;

            GetNextTarget();
        }
    }

    private void GetNextTarget()
    {
        if (targetDistributor.TargetsExist)
            Attack(targetDistributor.GetTargetFor(gameObject));
    }
}
