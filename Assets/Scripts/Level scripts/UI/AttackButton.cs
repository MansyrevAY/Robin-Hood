using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public GameEventSO attackEvent;

    public void LaunchAttack()
    {
        attackEvent.Raise();
    }
}
