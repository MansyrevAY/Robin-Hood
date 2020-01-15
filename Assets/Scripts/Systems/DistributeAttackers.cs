using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeAttackers : MonoBehaviour
{
    public GameObjRuntimeSetSO allGuards;
    public GameObjRuntimeSetSO allHoods;

    public void CreatePairsToFight()
    {
        GameObject closestGuard = null;
        HoodBehaviour hoodBehaviour = null;

        foreach (GameObject hood in allHoods.set)
        {
            closestGuard = GetClosestTo(hood, allGuards.set);
            hoodBehaviour = hood.GetComponent<HoodBehaviour>();

            hoodBehaviour.Attack(closestGuard);
        }
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
