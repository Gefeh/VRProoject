using UnityEngine;

public class CrocodileStateMachine : StateMachine<CrocodileStateMachine.States>
{
    [HideInInspector] public Crocodile crocodile;

    public enum States
    {
        Idle,
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
