using UnityEngine;

public class ApproachingCrocodileState : BaseState<CrocodileStateMachine.States>
{
    protected CrocodileStateMachine _machine;

    private bool _hasReachedTarget = false;

    public ApproachingCrocodileState(CrocodileStateMachine.States key, CrocodileStateMachine machine) : base(key)
    {
        this._machine = machine;
    }

    public override void EnterState()
    {
        _machine.crocodile.ApproachBar();
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
        if (!_machine.crocodile.NavMeshAgent.pathPending &&
            _hasReachedTarget)
        {
            return CrocodileStateMachine.States.Ordering;
        }

        return CrocodileStateMachine.States.Approaching;
    }

    public override void UpdateState()
    {
        
    }

    public override void OnTriggerReceived(Collider other)
    {
        if (other.CompareTag("Bar"))
        {
            _hasReachedTarget = true;
        }
    }
}
