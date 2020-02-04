using UnityEngine;

public class SquadNavigation : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject[] guards;
    public GameObject cart;

    private Vector3[] waypointCoordinates;
    private MovementBehaviour cartMovement;

    void Awake()
    {
        cartMovement = cart.GetComponent<MovementBehaviour>();
        waypointCoordinates = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointCoordinates[i] = waypoints[i].position;
        }
    }

    private void Start()
    {

    }

    // Called by PatrolManager
    public void StartSquadMoving()
    {
        cartMovement.ChangeDestination(waypointCoordinates);

        foreach (GameObject guard in guards)
        {
            MovementBehaviour guardMovement = guard.GetComponent<MovementBehaviour>();

            guardMovement.ChangeDestination(AdjustPathForGuard(guard));
        }
    }

    private Vector3[] AdjustPathForGuard(GameObject guard)
    {
        Vector3[] modifiedWaypoints = new Vector3[waypoints.Length];

        // positions are null when array is initialized
        for (int i = 0; i < modifiedWaypoints.Length; i++)
        {
            modifiedWaypoints[i] = Vector3.zero;
        }

        Vector3 delta = guard.transform.position - cart.transform.position;

        for (int i = 0; i < waypoints.Length; i++)
        {
            modifiedWaypoints[i] = waypoints[i].position + delta;
        }

        return modifiedWaypoints;
    }

    // Responce for AttackEvent
    public void ReadyForAttack()
    {
        cartMovement.StopMovement();
        
        foreach (GameObject guard in guards)
        {
            MovementBehaviour guardMovement = guard.GetComponent<MovementBehaviour>();

            guardMovement.StopMovement();
        }
    }
}
