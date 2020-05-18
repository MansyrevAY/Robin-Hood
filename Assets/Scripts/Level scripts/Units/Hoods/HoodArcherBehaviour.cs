using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SphereCollider))]
public class HoodArcherBehaviour : AttackBehaviour
{
    public DistributeAttackers targetDistributor;
    public float attackRange;
    public GameObject arrow;
    public Transform arrowPoint;

    private static GameObject _arrowAnchor;
    public static GameObject ArrowAnchor
    {
        get
        {
            if(_arrowAnchor == null)
            {
                _arrowAnchor = new GameObject("Arrow anchor");
            }

            return _arrowAnchor;
        }
    }
    public float arrowForce;

    private MovementBehaviour movement;

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
        => (currentTarget.transform.position - transform.position).magnitude <= GetComponent<SphereCollider>().radius ? true : false;

    // Start is called before the first frame update
    void Start()
    {
        SetBaseStats();
    }

    protected override void SetBaseStats()
    {
        movement = GetComponent<MovementBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == currentTarget)
        {
            movement.StopMovement();
            
            targetDamagable = currentTarget.GetComponent<HealthBehaviour>();
            InCombat = true;

            MakeAttack();
        }
    }

    public override void MakeAttack()
    {
        if (currentTarget == null || targetDamagable == null || !currentTarget.activeInHierarchy)
        {
            InCombat = false;
            GetNextTarget();
        }

        else
        {
            SpawnArrow();
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

    protected override void GetNextTarget()
    {
        Attack(targetDistributor.GetTargetFor(gameObject));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
