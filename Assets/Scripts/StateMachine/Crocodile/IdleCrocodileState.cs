using UnityEngine;

public class IdleCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    public IdleCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.PlayAnimation("Crocodile_Walk");
    }

    public override void ExitState()
    {
        _machine.crocodile.ResetIdleTimer();
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        if (_machine.crocodile.IdleTimer >= _machine.crocodile.TimeToWait)
        {
            return CrocodileStateMachine.States.Wandering;
        }
        else if (_machine.crocodile.Satisfaction < _machine.crocodile.ThirstThreshold)
        {
            return CrocodileStateMachine.States.Approaching;
        }
        
        return CrocodileStateMachine.States.Idle;
    }

    public override void UpdateState()
    {
        _machine.crocodile.IncrementIdleTimer();
        _machine.crocodile.ReduceSatisfaction();
    }

}
