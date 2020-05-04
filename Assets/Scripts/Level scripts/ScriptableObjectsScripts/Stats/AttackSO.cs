using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Stat", menuName = "Scriptable objects/Stats/Attack Stat")]
[System.Serializable]
public class AttackSO : ScriptableObject
{
    public int damage;

    [Tooltip("1 is normal speed")]
    [Range(0,2)]
    public float attackSpeed;
}
