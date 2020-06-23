using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Stat", menuName = "Scriptable objects/Stats/Attack Stat")]
[System.Serializable]
public class AttackSO : ScriptableObject
{
    public int damage;

    [Tooltip("1 is normal speed")]
    [Range(0,3)]
    public float attackSpeed;

    public float AttackDuration
    {
        get
        {
            if (attackSpeed == 0)
                return 0;
            else
            {
                return 1 / attackSpeed * 1.5f;    
            }
            
        }
    }
}
