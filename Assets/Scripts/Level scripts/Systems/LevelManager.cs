using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObjRuntimeSetSO allGuards;
    public GameObjRuntimeSetSO allHoods;
    public LevelStateSO gameState;

    private void Awake()
    {
        gameState.StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLoseScenario();
        CheckWinScenario();        
    }

    private void CheckLoseScenario()
    {
        if (gameState.State.Equals(typeof(AttackState)) && allGuards.set.Count == 0)
        {
            gameState.Win();
            Debug.Log("Wou won!");
        }
    }
    
    private void CheckWinScenario()
    {
        if (gameState.State.Equals(typeof(AttackState)) && allHoods.set.Count == 0)
        {
            gameState.Defeat();
            Debug.Log("You lost!");
        }
    }

    public void OnPatrolStarted() => gameState.BeginPatrol();
    public void OnAttackStarted() => gameState.BeginAttack();
}