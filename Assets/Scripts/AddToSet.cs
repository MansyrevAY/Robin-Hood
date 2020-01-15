using UnityEngine;

public class AddToSet : MonoBehaviour
{
    public GameObjRuntimeSetSO runtimeSet;

    private void OnEnable()
    {
        runtimeSet.Add(gameObject);
    }

    private void OnDisable()
    {
        runtimeSet.Remove(gameObject);
    }
}
