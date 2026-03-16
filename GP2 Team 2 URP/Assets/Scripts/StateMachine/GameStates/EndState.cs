using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndState : GameState
{
    public override void EnterState()
    {
        base.EnterState();
        PlayerInputMapSwapper inputMapSwapper = myStateMachine.player.GetComponent<PlayerInputMapSwapper>();
        if (inputMapSwapper != null)
        {
            inputMapSwapper.EnableUiInputs();
        }

        //myStateMachine.UiController.EnterUiState("EndState");
        Time.timeScale = 0f;

    }

    public override void UpdateState()
    {
        base.UpdateState();
        //string currentUi = myStateMachine.UiController.CurrentMenuDisplayed.name;
        //if ( currentUi == "SettingsMenu" || currentUi == "CreditsMenu")
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        myStateMachine.UiController.GoToPreviousMenu();
        //    }
        //}
    }

    public override void ExitState()
    {
        base.ExitState();
        //myStateMachine.UiController.ExitUiState("EndState");
        //Time.timeScale = 1f;
    }
}

