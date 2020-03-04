using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Runtime Set", menuName = "Scriptable objects/Runtime set")]
public class GameObjRuntimeSetSO : ScriptableObject
{
    public List<GameObject> set = new List<GameObject>();

    public void Add(GameObject obj)
    {
        if(!set.Contains(obj))
            set.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        if (set.Contains(obj))
            set.Remove(obj);
    }
}
