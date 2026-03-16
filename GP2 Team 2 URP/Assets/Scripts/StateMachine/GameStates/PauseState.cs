using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseState : GameState
{

    public override void EnterState()
    {
        base.EnterState();
        Cursor.visible = true;
        //myStateMachine.UiController.EnterUiState("PauseState");
        Time.timeScale = 0f;

    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

    }
    public override void ExitState()
    {
        base.ExitState();
        //Time.timeScale = 1f;

        //myStateMachine.UiController.ExitUiState("PauseState");
    }
}
