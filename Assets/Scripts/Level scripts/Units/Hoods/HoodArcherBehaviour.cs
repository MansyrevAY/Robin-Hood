using System;
using System.Collections.Generic;
using UnityEngine;
using Tools;

[RequireComponent(typeof(SphereCollider))]
public class HoodArcherBehaviour : AttackBehaviour
{
    public DistributeAttackers targetDistributor;
    [SerializeField]
    private float _attackRange;
    public GameObject arrow;
    public Transform arrowPoint;

    private bool _encounteredTarget = false;
    private Timer _shootTimer;
    [SerializeField]
    private SphereCollider _attackRadiusCollider;
    
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

    private MovementBehaviour _movement;

    #region Unity Events
    private void Awake()
    {
        _shootTimer = new Timer();
        _movement = GetComponent<MovementBehaviour>();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    private void Update()
    {
        CheckTarget();
        
        if(_shootTimer.IsActive)
            _shootTimer.Update(Time.deltaTime);
    }
    #endregion // Unity Events
    
    private void CheckTarget()
    {
        if (_encounteredTarget)
            return;
        if (!currentTarget)
            return;

        if (TargetIsInRange())
        {
            _encounteredTarget = true;
            _movement.StopMovement();

            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
            InCombat = true;
            
            MakeAttack();
        }
    }
    
    private bool TargetIsInRange()
        => TargetIsInRange(currentTarget.transform); // TODO: переделать ренжу со сферы на обычное расстояние и рисовать гизмо
    
    private bool TargetIsInRange(Transform target)
        => Vector3.Distance(target.position, transform.position) <= _attackRange;

    public override void Attack(GameObject target)
    {
        currentTarget = target;
        if(!target)
            return;

        if (TargetIsInRange())
        {
            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
            InCombat = true;
            MakeAttack();
        }
        
        else
        {
            _encounteredTarget = false;
            _movement.Chase(target);
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
            if (TargetIsInRange())
            {
                SpawnArrow();
                _shootTimer.StartTimer(originalAttack.AttackDuration, MakeAttack);
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
    
    protected override void SetBaseStats()
    {
        throw new NotImplementedException();
    }
}
