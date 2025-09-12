using UnityEngine;

public class CrocodileStateMachine : StateMachine<CrocodileStateMachine.States>
{
    [HideInInspector] public Crocodile crocodile;

    public enum States
    {
        Idle,
        Wandering,
        Approaching,
        Ordering,
        Hostile,
        Attacking,
        Hurt,
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
        _States.Add(States.Wandering, new WanderingCrocodileState(States.Wandering, this));

        _CurrentState = _States[States.Idle];
    }

}
