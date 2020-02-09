using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUnitToSpawn : MonoBehaviour
{
    [Tooltip("Scriptable object holding selected unit")]
    public GameObjSO unitHolder;
    public GameObject unitToReplace;

    void Awake()
    {
        if (unitToReplace == null)
            Debug.LogError("Unit to replace must be not null");
    }

    public void ReplaceUnit()
    {
        unitHolder.gameObject = unitToReplace;
    }
}
