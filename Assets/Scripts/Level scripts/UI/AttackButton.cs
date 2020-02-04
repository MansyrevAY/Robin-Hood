using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public GameEventSO attackEvent;

    public void LaunchAttack()
    {
        attackEvent.Raise();
    }
}
