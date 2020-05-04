using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovementBehaviour))]
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
            ShouldUpdatePosition = false;
        }
    }

    private void LateUpdate()
    {
        if (targetDistributor.TargetsExist && ShouldUpdatePosition)
        {
            movement.ChangeDestination(currentTarget.transform.position);
        }
    }

    public override void Attack(GameObject guard)
    {
        currentTarget = guard;
        movement.ChangeDestination(guard.transform.position);
        targetDamagable = currentTarget.GetComponent<HealthBehaviour>();

        ShouldUpdatePosition = true;
    }

    IEnumerator updateTarget(Transform guardPosition)
    {
        while (true)
        {
            yield return new WaitForSeconds(targetUpdateSpeed);
            movement.ChangeDestination(guardPosition.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            InCombat = true;
            movement.StopMovement();
            ShouldUpdatePosition = false;
            transform.LookAt(other.transform);
        }
    }

    protected override void GetNextTarget()
    {
        if (!targetDistributor.TargetsExist)
            ShouldUpdatePosition = false;
        else
            Attack(targetDistributor.GetTargetFor(gameObject));
    }
}
