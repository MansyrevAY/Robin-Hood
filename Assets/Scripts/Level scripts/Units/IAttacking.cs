using UnityEngine;

/// <summary>
/// Interface for making attacks and getting new targets
/// </summary>
public interface IAttacking
{
    void Attack(GameObject target);
}
