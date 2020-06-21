using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Tools;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(SphereCollider))]
public class HoodArcherBehaviour : AttackBehaviour
{
    public DistributeAttackers targetDistributor;
    [SerializeField]
    private float attackRange;
    public GameObject arrow;
    public Transform arrowPoint;

    private bool _encounteredTarget = false;
    private Timer _shootTimer;
    [SerializeField]
    private SphereCollider attackRadiusCollider;
    
    private static GameObject _arrowAnchor;
    private static GameObject ArrowAnchor
    {
        get
        {
            if(ReferenceEquals(null, _arrowAnchor))
            {
                _arrowAnchor = new GameObject("Arrow anchor");
            }

            return _arrowAnchor;
        }
    }
    public float arrowForce;

    private MovementBehaviour movement;
    private List<GameObject> _enemiesInRange;
    
    private void Awake()
    {
        _shootTimer = new Timer();
        _enemiesInRange = new List<GameObject>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
        CheckTarget();
        
        if(_shootTimer.IsActive)
            _shootTimer.Update(Time.deltaTime);
    }

    public override void Attack(GameObject target)
    {
        currentTarget = target;

        if (TargetInRadius())
        {
            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
            InCombat = true;
            MakeAttack();
        }
        else
        {
            movement.Chase(target);
        }
    }

    private bool TargetInRadius()
        => TargetInRadius(currentTarget.transform); // TODO: переделать ренжу со сферы на обычное расстояние и рисовать гизмо
    
    private bool TargetInRadius(Transform target)
        => Vector3.Distance(target.position, transform.position) <= attackRange;

    // Start is called before the first frame update
    void Start() => SetBaseStats();

    protected override void SetBaseStats() => movement = GetComponent<MovementBehaviour>();

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentTarget = targetDistributor.GetTargetFor(gameObject);
            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
            _shootTimer.StartTimer(originalAttack.attackSpeed, MakeAttack);
            InCombat = true;
        }

        if (!TargetInRadius())
        {
            movement.StopMovement();
            InCombat = false;
        }
    }

    private void CheckTarget()
    {
        if (_encounteredTarget)
            return;
        if (!currentTarget)
            return;

        if (TargetInRadius())
        {
            movement.StopMovement();

            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
            InCombat = true;

            MakeAttack();
        }
    }

    public override void MakeAttack()
    {
        if (ReferenceEquals(null, currentTarget) || ReferenceEquals(null, targetDamagable) || !currentTarget.activeInHierarchy)
        {
            InCombat = false;
            GetNextTarget();
            _encounteredTarget = false;
        }

        else
        {
            if (TargetInRadius())
            {
                _encounteredTarget = true;
                SpawnArrow();
                _shootTimer.StartTimer(originalAttack.attackSpeed, MakeAttack);
            }
            
            else
                GetNextTarget();
        }
    }

    private void SpawnArrow()
    {
        Vector3 predictedPosition = AIShoot.AimAtTarget(arrowPoint.gameObject, currentTarget, (arrowPoint.forward * arrowForce).magnitude);
        transform.LookAt(predictedPosition);

        GameObject arrowClone = Instantiate(arrow, arrowPoint.position, arrowPoint.rotation, ArrowAnchor.transform);
        
        arrowClone.GetComponent<Arrow>().Init(originalAttack.damage);
        arrowClone.GetComponent<Rigidbody>().velocity = arrowPoint.forward * arrowForce;
    }

    protected override void GetNextTarget() => Attack(targetDistributor.GetTargetFor(gameObject));
}
