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
        
    }

    public override void ExitState()
    {
        
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        return CrocodileStateMachine.States.Idle;
    }

    public override void UpdateState()
    {
        
    }

}
