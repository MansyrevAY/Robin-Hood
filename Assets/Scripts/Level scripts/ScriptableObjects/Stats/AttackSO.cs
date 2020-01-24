using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Stat", menuName = "Scriptable objects/Stats/Attack Stat")]
public class AttackSO : ScriptableObject
{
    public int damage;
    public float attackSpeed;
}
