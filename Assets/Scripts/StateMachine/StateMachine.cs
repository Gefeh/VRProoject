using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    public string state;

    protected Dictionary<EState, BaseState<EState>> _States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> _CurrentState;

    void Start()
    {
        _CurrentState.EnterState();
        state = _CurrentState.ToString();
    }

    void Update()
    {
        EState nextStateKey = _CurrentState.GetNextState();

        if (nextStateKey.Equals(_CurrentState.StateKey))
        {
            _CurrentState.UpdateState();
        }
        else
        {
            TransitionToState(nextStateKey);
        }
    }

    /// <summary>
    /// Transition from current state to given state.
    /// </summary>
    /// <param name="stateKey"></param>
    public void TransitionToState(EState stateKey)
    {
        _CurrentState.ExitState();
        _CurrentState = _States[stateKey];
        _CurrentState.EnterState();
        state = stateKey.ToString();
    }
}

