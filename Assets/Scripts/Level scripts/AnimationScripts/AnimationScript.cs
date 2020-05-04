//#define DEBUG_MODE

using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class AnimationScript : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected AttackBehaviour attack;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<AttackBehaviour>();
    }

    void Update() => SetAnimations();

    private void SetAnimations()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;

        #if DEBUG_MODE
        Debug.Log("Speed = " + speedPercent + "%");
        #endif

        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if(attack.InCombat)
            SetAttackSpeed();

        if (animator.GetBool("inCombat") != attack.InCombat)
            animator.SetBool("inCombat", attack.InCombat);
    }

    private void SetAttackSpeed()
    {
        AnimatorController ac = animator.runtimeAnimatorController as AnimatorController;

        AnimatorStateMachine sm = ac.layers[0].stateMachine;

        foreach (ChildAnimatorState child in sm.states)
        {
            if (child.state.speed != attack.originalAttack.attackSpeed && child.state.name.Contains("attack"))
                child.state.speed = attack.originalAttack.attackSpeed;
        }
    }


}
