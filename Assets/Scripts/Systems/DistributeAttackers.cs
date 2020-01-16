using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeAttackers : MonoBehaviour
{
    public GameObjRuntimeSetSO allGuards;
    public GameObjRuntimeSetSO allHoods;

    public bool TargetsExist
    {
        get { return allGuards.set.Count > 0 ? true : false; }
    }

    public void DistributeHoodTargets()
    {
        GameObject closestGuard = null;
        //HoodBehaviour hoodBehaviour = null;

        foreach (GameObject hood in allHoods.set)
        {
            closestGuard = GetClosestTo(hood, allGuards.set);
            
            //Т.к. не увидел, чтобы hoodBehaviour использовался где-то еще, убрал его.
            hood.GetComponent<HoodBehaviour>().Attack(closestGuard);

        }
    }

    public GameObject GetTargetFor(GameObject hood)
    {
        GameObject pair = null;

        if (allHoods.set.Contains(hood))            
        {
            pair = GetClosestTo(hood, allGuards.set);
        }

        return pair;
    }

    private GameObject GetClosestTo(GameObject obj, List<GameObject> inList)
    {
        float delta = Mathf.Infinity;
        float newDelta = 0f;

        GameObject closestObj = null;

        foreach (GameObject neighboor in inList)
        {
            newDelta = Vector3.Distance(obj.transform.position, neighboor.transform.position);

            if(newDelta < delta)
            {
                delta = newDelta;
                closestObj = neighboor;
            }
        }

        return closestObj;
    }
}
