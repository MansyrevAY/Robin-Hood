using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementBehaviour : MonoBehaviour
{
    // Сделать остановку на дельте, а не точной
    protected Vector3[] waypoints;
    protected int currentWaypoint = 0;
    protected NavMeshAgent navigationAgent;

    private bool singleWaypoint = false;

    // Start is called before the first frame update
    void Awake()
    {
        navigationAgent = GetComponent<NavMeshAgent>();
        navigationAgent.isStopped = true;

        waypoints = new Vector3[0];
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfReachedWaypoint();
    }

    protected void CheckIfReachedWaypoint()
    {
        if (navigationAgent.isStopped)
            return;

        if (waypoints.Length == 0 || currentWaypoint == waypoints.Length - 1)
            return;

        if (IsAtDestination())
        {
            if(singleWaypoint)
            {
                singleWaypoint = false;
                return;
            }


            currentWaypoint++;
            navigationAgent.SetDestination(waypoints[currentWaypoint]);
        }
    }

    private bool IsAtDestination()
    {
        if (gameObject.transform.position.x == navigationAgent.destination.x && gameObject.transform.position.z == navigationAgent.destination.z)
            return true;
        else
            return false;
    }

    public void ChangeDestination(Vector3 destination)
    {
        navigationAgent.isStopped = false;
        navigationAgent.updatePosition = true;

        navigationAgent.SetDestination(destination);
        singleWaypoint = true;
    }

    public void ChangeDestination(Vector3[] waypoints)
    {
        navigationAgent.isStopped = false;
        navigationAgent.updatePosition = true;

        this.waypoints = waypoints;
        currentWaypoint = 0;
        navigationAgent.SetDestination(this.waypoints[currentWaypoint]);
    }

    public void StopMovement()
    {
        navigationAgent.isStopped = true;
        navigationAgent.updatePosition = false;


        NavMeshObstacle obs = GetComponent<NavMeshObstacle>();
        if (obs != null)
            obs.enabled = true;
    }
}
