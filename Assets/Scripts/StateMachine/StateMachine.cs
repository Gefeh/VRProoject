using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    [Tooltip("The current state of the machine.")]
    [SerializeField]
    public string currentStateName;

    [Tooltip("Use this to manually transition to a new state from the Inspector.")]
    public EState transitionToState;
    private EState _previousTransitionState;

    protected Dictionary<EState, BaseState<EState>> _States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> _CurrentState;

    protected virtual void Start()
    {
        if (_CurrentState != null)
        {
            _CurrentState.EnterState();
            UpdateInspectorStateName();
            transitionToState = _CurrentState.StateKey;
            _previousTransitionState = _CurrentState.StateKey;
        }
    }

    protected virtual void Update()
    {
        if (!transitionToState.Equals(_previousTransitionState))
        {
            TransitionToState(transitionToState);
            _previousTransitionState = transitionToState;
            return;
        }

        if (_CurrentState == null) return;

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
    /// Transition from current state to the given state.
    /// </summary>
    /// <param name="stateKey"></param>
    public void TransitionToState(EState stateKey)
    {
        if (!_States.ContainsKey(stateKey))
        {
            Debug.LogWarning($"State {stateKey} not found in the state machine.");
            return;
        }

        if (_CurrentState != null)
        {
            _CurrentState.ExitState();
        }

        _CurrentState = _States[stateKey];
        _CurrentState.EnterState();

        UpdateInspectorStateName();

        transitionToState = stateKey;
        _previousTransitionState = stateKey;
    }

    /// <summary>
    /// Updates the string field used for display in the inspector.
    /// </summary>
    private void UpdateInspectorStateName()
    {
        if (_CurrentState != null)
        {
            currentStateName = _CurrentState.StateKey.ToString();
        }
    }
}