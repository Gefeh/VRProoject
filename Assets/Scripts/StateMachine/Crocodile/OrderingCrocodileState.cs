using UnityEngine;

public class OrderingCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    public OrderingCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.PlayAnimation("Crocodile_Sit");
    }

    public override void ExitState()
    {
        
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        return CrocodileStateMachine.States.Ordering;
    }

    public override void UpdateState()
    {
        _machine.crocodile.LookAtPlayer();
    }

}
