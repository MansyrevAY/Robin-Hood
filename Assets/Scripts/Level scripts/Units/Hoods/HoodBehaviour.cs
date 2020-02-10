using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementBehaviour))]
public class HoodBehaviour : AttackBehaviour
{
    public DistributeAttackers targetDistributor;
    [Tooltip("How often is target positon updated")]
    public float targetUpdateSpeed;

    private MovementBehaviour movement;
    private IEnumerator updateCoroutine;

    private void Awake()
    {
        movement = GetComponent<MovementBehaviour>();
        SetBaseStats();
    }

    protected override void SetBaseStats()
    {
        damage = originalAttack.damage;
        attackDuration = originalAttack.attackSpeed;
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
        movement.ChangeDestination(guard.transform.position);
        targetDamagable = currentTarget.GetComponent<HealthBehaviour>();

        updateCoroutine = updateTarget(guard.transform);
        StartCoroutine(updateCoroutine);
    }

    IEnumerator updateTarget(Transform guardPosition)
    {
        while(true)
        {
            movement.ChangeDestination(guardPosition.position);
            yield return new WaitForSeconds(targetUpdateSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            InCombat = true;
            movement.StopMovement();
            StopCoroutine(updateCoroutine);
            transform.LookAt(other.transform);
        }
    }

    protected override void GetNextTarget()
    {
        if (targetDistributor.TargetsExist)
            Attack(targetDistributor.GetTargetFor(gameObject));
    }
}
