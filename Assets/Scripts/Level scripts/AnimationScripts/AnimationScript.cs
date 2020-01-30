//#define DEBUG_MODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationScript : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected AttackBehaviour attack;
    protected  List<animationTime> attackAnimations;
    protected int currentAttack = 0;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<AttackBehaviour>();
        GetAnimationLengths();
    }

    protected void GetAnimationLengths()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        
        attackAnimations = new List<animationTime>();

        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name.Contains("attack"))
            {
                attackAnimations.Add(new animationTime { name = clips[i].name, duration = clips[i].length });
            }
        }
    }

    void Update() => SetAnimations();

    private void SetAnimations()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;

        #if DEBUG_MODE
        Debug.Log("Speed = " + speedPercent + "%");
        #endif

        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if (animator.GetBool("inCombat") != attack.InCombat)
            animator.SetBool("inCombat", attack.InCombat);

        if (!attack.InCombat)
        {
            animator.SetInteger("attackNum", -1);
            currentAttack = 0;
        }
    }

    public void PlayNextAttackAnimation()
    {
        animator.SetInteger("attackNum", currentAttack);
        //currentAttack++;

        if (currentAttack == attackAnimations.Count - 1)
            currentAttack = 0;
    }

    public float GetNextAttackDuration()
    {
        return attackAnimations[currentAttack].duration;
    }
}

public struct animationTime
{
    public string name;
    public float duration;
};
