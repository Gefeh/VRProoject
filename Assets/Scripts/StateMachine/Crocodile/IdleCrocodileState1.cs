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
        
    }

    public override void ExitState()
    {
        
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        return CrocodileStateMachine.States.Wandering;
    }

    public override void UpdateState()
    {
        
    }

}
