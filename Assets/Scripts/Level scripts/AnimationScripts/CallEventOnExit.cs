using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

public class CallEventOnExit : StateMachineBehaviour
{
    public UnityEvent Event;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Event.Invoke();
    }
}
