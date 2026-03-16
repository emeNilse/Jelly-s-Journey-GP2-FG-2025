using UnityEngine;
using UnityEngine.Events;

public class GameState : State
{
    public UnityEvent StateUpdate;
    public UnityEvent StateFixedUpdate;

    protected GameManager myStateMachine;

    public void SetParentStateMachine()
    {
        myStateMachine = GameManager.Instance;
    }

    public virtual void Start()
    {
        SetParentStateMachine();
    }
    public override void EnterState()
    {
        base.EnterState();
        if(myStateMachine == null) SetParentStateMachine();
        myStateMachine.StateEnter?.Invoke(this);
    }

    public override void ExitState()
    {
        base.ExitState();
        if (myStateMachine == null) SetParentStateMachine();
        myStateMachine.StateExit?.Invoke(this);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        StateUpdate?.Invoke();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        StateFixedUpdate?.Invoke();
    }
}
