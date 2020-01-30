using UnityEngine;

public class DestroyOnDisable : MonoBehaviour
{
    private void OnDisable()
    {
        Invoke("DeleteKilled", 2f);
    }

    private void DeleteKilled()
    {
        Destroy(gameObject);
    }
}
