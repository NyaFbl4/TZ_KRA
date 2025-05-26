using System;
using System.Collections.Generic;
using UnityEngine;

namespace TZ.Enemy_FSM
{
    public class EnemyFSM
    {
        private EnemyStateFSM CurrentState;
        
        private Dictionary<Type, EnemyStateFSM> _enemyStates = new ();

        public void AddState(EnemyStateFSM state)
        {
            _enemyStates.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : EnemyStateFSM
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
            {
                return;
            }

            if (_enemyStates.TryGetValue(type, out var newState))
            {
                CurrentState?.ExitState();

                CurrentState = newState;
                CurrentState.EnterState(); 
            }
        }

        public void Update()
        {
            CurrentState?.UpdateState();
        }
    }
}