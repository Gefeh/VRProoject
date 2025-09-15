using UnityEngine;

public class LeavingCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    public LeavingCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.PlayAnimation("Crocodile_Leave");
    }

    public override void ExitState()
    {
        _machine.crocodile.ReturnToSpawn();
    }

    public override CrocodileStateMachine.States GetNextState()
    {
        // Transitions OUT to Wandering state after Crocodile_Leave animation is done via animation event

        return CrocodileStateMachine.States.Leaving;
    }

    public override void UpdateState()
    {
        _machine.crocodile.ReduceSatisfaction();
    }

}
