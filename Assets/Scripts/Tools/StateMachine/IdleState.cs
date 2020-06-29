using System;
using UnityEngine;

namespace Tools.StateMachine
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "State/Idle State")]
    [Obsolete]
    public class IdleState : BaseState
    {
        // public IdleState(GameObject gameObject) : base(gameObject)
        // {
        // }

        public override void Init(GameObject gameObject)
        {
            base.Init(gameObject);
        }

        public override Type Update()
        {
            throw new NotImplementedException();
        }
    }
}