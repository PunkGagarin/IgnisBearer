using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Units;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.GameStates
{
    public class SimpleStateMachine<T> : IGameStateMachine<T>
        , IPayloadStateMachine<T>
    {

        protected T _currentState;
        protected Dictionary<Type, T> _states = new();

        public void Register(T state)
        {
            Debug.Log($" Register state {state.GetType().Name}");
            _states.Add(state.GetType(), state);
        }

        public void Enter<TState>() where TState : class, T, IState
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {typeof(TState).Name}");
            IState state = ChangeCurrentState<TState>();
            state.Enter();
        }

        public void Enter(Type stateType)
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {stateType.GetType().Name}");
            var state = ChangeCurrentState(stateType) as IState;
            state?.Enter();
        }

        public void Enter<TPayload>(Type stateType, TPayload payload)
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {stateType.GetType().Name}");
            var state = ChangeCurrentState(stateType) as IPayloadState<TPayload>;
            state?.Enter(payload);
        }


        public void Enter<TState, TPayload>(TPayload payload) where TState : class, T, IPayloadState<TPayload>
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {typeof(TState).Name}");
            TState state = ChangeCurrentState<TState>();
            state.Enter(payload);
        }

        public void Enter<TState, TNextState, TPayload, TNextPayload>(TPayload payload, TNextPayload nextPayload)
            where TState : class, T, IEnterWithPayloadAndNextPayload<TPayload>
            where TNextState : class, IPayloadState<TNextPayload>, IUnitState
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {typeof(TState).Name}");
            TState state = ChangeCurrentState<TState>();
            state.Enter<TNextState, TNextPayload>(payload, nextPayload);
        }
        
        public void Enter<TState, TNextState, TPayload>(TPayload payload)
            where TState : class, T, IEnterWithNext<TPayload>
            where TNextState : class, IState, IUnitState
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {typeof(TState).Name}");
            TState state = ChangeCurrentState<TState>();
            state.Enter<TNextState>(payload);
        }

        public void Enter<TState, TPayload, TPayload2>(TPayload payload, TPayload2 payload2)
            where TState : class, T, IPayloadState<TPayload, TPayload2>
        {
            // Debug.Log($"Tryint to change state from  {_currentState?.GetType().Name} to {typeof(TState).Name}");
            TState state = ChangeCurrentState<TState>();
            state.Enter(payload, payload2);
        }

        protected T ChangeCurrentState(Type nextState)
        {
            if (_currentState is IExitableState state)
                state.Exit();

            var newState = _states[nextState];
            _currentState = newState;
            return newState;
        }


        protected TState ChangeCurrentState<TState>() where TState : class, T, IExitableState
        {
            if (_currentState is IExitableState state)
                state.Exit();

            var newState = _states[typeof(TState)] as TState;
            _currentState = newState;
            return newState;
        }
    }
}