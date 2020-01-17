using UnityEngine;

public class SquadNavigation : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject[] guards;
    public GameObject cart;

    private Vector3[] waypointCoordinates;
    private MoveToLocation cartMovement;
    private MoveToLocation[] guardMovements; // death of guards needs to be handled

    void Awake()
    {
        cartMovement = cart.GetComponent<MoveToLocation>();
        waypointCoordinates = new Vector3[waypoints.Length];
        guardMovements = new MoveToLocation[guards.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointCoordinates[i] = waypoints[i].position;
        }

        // Handle death of the guards first
        //for (int i = 0; i < guardMovements.Length; i++)
        //{
        //    guardMovements[i] = guards[i].GetComponent<MoveToLocation>();
        //}
    }

    private void Start()
    {
        //StartSquadMoving();
    }


    public void StartSquadMoving()
    {
        cartMovement.ChangeDestination(waypointCoordinates);

        foreach (GameObject guard in guards)
        {
            MoveToLocation guardMovement = guard.GetComponent<MoveToLocation>();

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

    public void ReadyForAttack()
    {
        cartMovement.StopMovement();

        
        foreach (GameObject guard in guards)
        {
            MoveToLocation guardMovement = guard.GetComponent<MoveToLocation>();

            guardMovement.StopMovement();
        }
    }
}
