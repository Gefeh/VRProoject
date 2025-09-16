using UnityEngine;

public class HostileCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    public HostileCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.PlayAnimation("Crocodile_Walk");
        _machine.crocodile.ApproachPlayer();
    }

    public override void ExitState()
    {
        _machine.crocodile.ResetIdleTimer();
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        if (_machine.crocodile.nearPlayer)
        {
            return CrocodileStateMachine.States.Attacking;
        }
        return CrocodileStateMachine.States.Hostile;
    }

    public override void UpdateState()
    {
        _machine.crocodile.ApproachPlayer();
    }

}
