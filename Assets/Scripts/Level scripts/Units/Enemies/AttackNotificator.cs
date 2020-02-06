using UnityEngine;

[RequireComponent(typeof(SquadNavigation))]
public class AttackNotificator : MonoBehaviour
{
    public GameObjRuntimeSetSO allPatrols;

    private bool hasStopped = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hood"))
        {
            TriggerAttack();
        }
    }

    /// <summary>
    /// Stops every patrol in allPatrols
    /// </summary>
    private void TriggerAttack()
    {
        foreach (GameObject patrol in allPatrols.set)
        {
            patrol.GetComponent<AttackNotificator>().StopPatrol();
        }
    }

    public void StopPatrol()
    {
        if (hasStopped)
            return;

        hasStopped = true;
        GetComponent<SquadNavigation>().ReadyForAttack();
    }
}
