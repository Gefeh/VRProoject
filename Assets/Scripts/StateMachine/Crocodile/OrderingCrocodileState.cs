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
        if (_machine.crocodile.Satisfaction >= _machine.crocodile.MaxSatisfaction)
        {
            return CrocodileStateMachine.States.Leaving;
        }
        else if (_machine.crocodile.Satisfaction < 0)
        {
            return CrocodileStateMachine.States.Hostile;
        }
            return CrocodileStateMachine.States.Ordering;
    }

    public override void UpdateState()
    {
        _machine.crocodile.LookAtPlayer();
        _machine.crocodile.ReduceSatisfaction();
    }

}
