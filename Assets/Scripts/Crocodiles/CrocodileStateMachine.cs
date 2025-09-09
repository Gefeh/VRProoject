using UnityEngine;

public class CrocodileStateMachine : StateMachine<CrocodileStateMachine.States>
{
    [HideInInspector] public Crocodile crocodile;

    public enum States
    {
        Idle,
        Wandering,
        Ordering,
        Hostile,
        Attacking,
        Dead,
    }

    void Awake()
    {
        InitializeStates();
    }

    void InitializeStates()
    {
        crocodile = GetComponent<Crocodile>();

        _States.Add(States.Idle, new IdleCrocodileState(States.Idle, this));

        _CurrentState = _States[States.Idle];
    }

}
