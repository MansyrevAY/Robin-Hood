using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools.StateMachine
{
    [Obsolete]
    public class StateMachine : MonoBehaviour
    {
        [SerializeField]
        private List<BaseState> states;
        
        private Dictionary<Type, BaseState> _states;
        private BaseState _currentState;
        
        private void Awake()
        {
            _states = new Dictionary<Type, BaseState>();
            foreach (BaseState state in states)
            {
                Type t = state.GetType();
                //_states.Add(t, BaseState.CreateInstance(t));
            }

            _currentState = states.First();
        }

        private void Update()
        {
            if(ReferenceEquals(null, _currentState))
                return;

            var nextStateType = _currentState.Update();

            if (!ReferenceEquals(_currentState.GetType(), nextStateType))
                _currentState = _states[nextStateType];
        }
    }
}
