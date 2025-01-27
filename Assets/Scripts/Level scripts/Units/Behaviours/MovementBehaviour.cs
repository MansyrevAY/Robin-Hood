﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementBehaviour : MonoBehaviour
{
    public float waypointTriggerDelta = 0.5f; // default, can be set in Inspector

    // Сделать остановку на дельте, а не точной
    protected Vector3[] waypoints;
    protected int currentWaypoint = 0;
    protected NavMeshAgent navigationAgent;

    private bool singleWaypoint = false;

    private GameObject currentTarget;
    private bool shouldUpdatePosition = false;
    protected bool ShouldUpdatePosition { get => shouldUpdatePosition & (currentTarget != null); set => shouldUpdatePosition = value; }


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

    private void LateUpdate()
    {
        if (ShouldUpdatePosition)
        {
            ChangeDestination(currentTarget.transform.position);
        }
    }

    protected void CheckIfReachedWaypoint()
    {
        if (!navigationAgent.enabled)
            return;

        if (navigationAgent.isStopped)
            return;

        if (waypoints.Length == 0 || currentWaypoint == waypoints.Length - 1)
            return;

        if (IsAtDestination())
        {
            if(singleWaypoint)
            {
                singleWaypoint = false;
                shouldUpdatePosition = false;
                return;
            }


            currentWaypoint++;
            navigationAgent.SetDestination(waypoints[currentWaypoint]);
        }
    }

    private bool IsAtDestination()
    {
        // Works better without y coordinate
        Vector2 unitPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        Vector2 destinationPosition = new Vector2(navigationAgent.destination.x, navigationAgent.destination.z);

        if (Vector2.Distance(unitPosition, destinationPosition) <  waypointTriggerDelta)
            return true;
        else
            return false;
    }

    public void ChangeDestination(Vector3 destination)
    {
        SetObstacleActive(false);

        if (navigationAgent.destination == destination)
            return;

        navigationAgent.isStopped = false;
        navigationAgent.updatePosition = true;

        navigationAgent.SetDestination(destination);
        singleWaypoint = true;
    }

    public void ChangeDestination(Vector3[] waypoints)
    {
        SetObstacleActive(false);

        navigationAgent.isStopped = false;
        navigationAgent.updatePosition = true;

        this.waypoints = waypoints;
        currentWaypoint = 0;
        navigationAgent.SetDestination(this.waypoints[currentWaypoint]);
    }

    public void Chase(GameObject target)
    {
        ChangeDestination(target.transform.position);

        currentTarget = target;
        shouldUpdatePosition = true;
    }

    public void StopMovement()
    {
        navigationAgent.isStopped = true;
        navigationAgent.updatePosition = false;
        ShouldUpdatePosition = false;

        SetObstacleActive(true);
    }

    private void SetObstacleActive(bool active)
    {
        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
        if (obstacle != null)
        {
            navigationAgent.enabled = !active;
            obstacle.enabled = active;
        }
    }
}
