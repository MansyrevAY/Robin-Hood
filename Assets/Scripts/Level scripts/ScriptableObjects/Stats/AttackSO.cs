using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Stat", menuName = "Scriptable objects/Stats/Attack Stat")]
[System.Serializable]
public class AttackSO : ScriptableObject
{
    [SerializeField]
    public int damage;
    public float attackSpeed;
}
