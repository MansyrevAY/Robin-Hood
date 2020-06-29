using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Tools.StateMachine
{
    [Obsolete]
    public abstract class BaseState : ScriptableObject
    {
        protected GameObject gameObject;
        protected Transform transform;
        
        // public BaseState(GameObject gameObject)
        // {
        //     this.gameObject = gameObject;
        //     this.transform = gameObject.transform;
        // }

        public virtual void Init(GameObject gameObject)
        {
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }

        public abstract Type Update();
    }
}
