using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToLocation : MonoBehaviour
{
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

        if (gameObject.transform.position.x == navigationAgent.destination.x && gameObject.transform.position.z == navigationAgent.destination.z)
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

    public void ChangeDestination(Vector3 destination)
    {
        navigationAgent.isStopped = false;

        navigationAgent.SetDestination(destination);
        singleWaypoint = true;
    }

    public void ChangeDestination(Vector3[] waypoints)
    {
        navigationAgent.isStopped = false;

        this.waypoints = waypoints;
        currentWaypoint = 0;
        navigationAgent.SetDestination(this.waypoints[currentWaypoint]);
    }

    public void StopMovement()
    {
        navigationAgent.isStopped = true;
    }
}
