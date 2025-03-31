using System;
using System.Collections.Generic;

namespace _2D.Scripts.FSM
{
    public class Fsm
    {
        public FsmState CurrentState { get; private set; }
        private readonly Dictionary<Type, FsmState> _states = new();

        public void AddState(FsmState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : FsmState
        {
            var type = typeof(T);

            if (CurrentState?.GetType() == type)
                return;

            if (_states.TryGetValue(type, out var newState))
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }

        public void Update(float deltaTime)
        {
            CurrentState?.Update(deltaTime);
        }
    }
}