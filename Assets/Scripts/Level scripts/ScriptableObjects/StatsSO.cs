using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Stats SO", menuName ="Scriptable objects/Stats")]
public class StatsSO : ScriptableObject
{
    public int maxHP;
    [Tooltip("In seconds of pause between attacks")]
    public float attackSpeed;
    public int damage;
}
