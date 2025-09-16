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
        Leaving,
        Hostile,
        Attacking,
        Dead,
    }

    void Awake()
    {
        InitializeStates();
    }
    private void OnTriggerEnter(Collider other)
    {
        _CurrentState?.OnTriggerReceived(other);
    }

    void InitializeStates()
    {
        crocodile = GetComponent<Crocodile>();

        _States.Add(States.Idle, new IdleCrocodileState(States.Idle, this));
        _States.Add(States.Wandering, new WanderingCrocodileState(States.Wandering, this));
        _States.Add(States.Ordering, new OrderingCrocodileState(States.Ordering, this));
        _States.Add(States.Approaching, new ApproachingCrocodileState(States.Approaching, this));
        _States.Add(States.Leaving, new LeavingCrocodileState(States.Leaving, this));
        _States.Add(States.Hostile, new HostileCrocodileState(States.Hostile, this));
        _States.Add(States.Attacking, new AttackingCrocodileState(States.Attacking, this));

        _CurrentState = _States[States.Idle];
    }
}
