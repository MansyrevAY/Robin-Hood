using System;
using UnityEngine;

/// <summary>
/// This is object for storing current level state
/// </summary>
[CreateAssetMenu(fileName = "LevelState", menuName ="Scriptable objects/Level State")]
public class LevelStateSO : ScriptableObject
{
    protected StateBase state;
    public Type State
    {
        get { return state.GetType(); }
    }

    [Tooltip("state for presentation only")]
    public string currentState;

    public void StartLevel() => state = new PreparationState();
    public void BeginPatrol() => state = new PatrolingState();    
    public void BeginAttack() => state = new AttackState();
    public void Defeat() => state = new DefeatState();
    public void Win() => state = new WinState();
}
