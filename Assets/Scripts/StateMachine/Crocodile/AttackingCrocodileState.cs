using UnityEngine;

public class AttackingCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    public AttackingCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.PlayAnimation("Crocodile_Idle_Bite");
    }

    public override void ExitState()
    {
        _machine.crocodile.ResetIdleTimer();
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        if (!_machine.crocodile.nearPlayer)
        {
            return CrocodileStateMachine.States.Hostile;
        }
        return CrocodileStateMachine.States.Attacking;
    }

    public override void UpdateState()
    {
        
    }

}
