using UnityEngine;

public class WanderingCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    public WanderingCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.PlayAnimation("Crocodile_Swim");
    }

    public override void ExitState()
    {
        if (_machine.crocodile.NavMeshAgent.hasPath)
        {
            _machine.crocodile.NavMeshAgent.ResetPath();
        }
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        if (_machine.crocodile.Satisfaction < _machine.crocodile.ThirstThreshold)
        {
            return CrocodileStateMachine.States.Approaching;
        }
        return CrocodileStateMachine.States.Wandering;
    }

    public override void UpdateState()
    {
        if (!_machine.crocodile.NavMeshAgent.pathPending && _machine.crocodile.NavMeshAgent.remainingDistance <= _machine.crocodile.NavMeshAgent.stoppingDistance)
        {
            _machine.crocodile.SetNewWanderDestination();
        }

        if (_machine.crocodile.IdleTimer >= _machine.crocodile.TimeToWait)
        {
            _machine.crocodile.SetNewWanderDestination();
        }

        _machine.crocodile.IncrementIdleTimer();
        _machine.crocodile.ReduceSatisfaction();
    }

}
