using UnityEngine;

public class AttackNotificator : MonoBehaviour
{
    public GameObjRuntimeSetSO allPatrols;
    public SquadNavigation squadNavigation;

    private bool hasStopped = false;

    void Awake()
    {
        if (squadNavigation == null)
            Debug.LogError("Squad navigation should be set to parent's patrol navigation");
    }

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
            //patrol.GetComponent<AttackNotificator>().StopPatrol();
            patrol.GetComponentInChildren<AttackNotificator>(false).StopPatrol();
        }
    }

    public void StopPatrol()
    {
        if (hasStopped)
            return;

        hasStopped = true;
        squadNavigation.ReadyForAttack();
    }
}
