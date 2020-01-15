using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveToLocation))]
public class HoodBehaviour : MonoBehaviour
{
    public int damage = 20;
    public float attackSpeed = 0.5f;

    private MoveToLocation hoodMovement;
    private GameObject attackTarget;
    private IDamagable targetDamagable;
    private bool shouldDealDamage = false;

    private float lastAttacktime = 0;

    void Awake()
    {
        hoodMovement = GetComponent<MoveToLocation>();
    }

    private void Update()
    {
        if (shouldDealDamage && Time.time - lastAttacktime > attackSpeed)
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            targetDamagable = attackTarget.GetComponent<IDamagable>();
            shouldDealDamage = true;
            //StartCoroutine(HitAttackTarget());
        }
    }

    private void HitAttackTarget()
    {
        bool condition = targetDamagable.TakeDamage(damage);

        if (condition)
            shouldDealDamage = false;
    }
}
