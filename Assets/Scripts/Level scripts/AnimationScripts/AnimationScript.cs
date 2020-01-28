﻿//#define DEBUG_MODE

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

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        attack = GetComponent<AttackBehaviour>();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;

        #if DEBUG_MODE
        Debug.Log("Speed = " + speedPercent + "%");
        #endif

        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        if(animator.GetBool("inCombat") != attack.InCombat)
            animator.SetBool("inCombat", attack.InCombat);
        
    }
}
