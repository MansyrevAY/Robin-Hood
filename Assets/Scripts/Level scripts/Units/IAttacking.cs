using UnityEngine;

/// <summary>
/// Interface for making attacks and getting new targets
/// </summary>
public interface IAttacking
{
    /// <summary>
    /// Start attacking target
    /// </summary>
    /// <param name="target"></param>
    void Attack(GameObject target);
}
