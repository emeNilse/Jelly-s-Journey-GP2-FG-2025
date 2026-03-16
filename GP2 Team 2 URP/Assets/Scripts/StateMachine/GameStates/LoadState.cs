using RoomSystem;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadState : GameState
{
    private Scene _previousScene;
    private Scene _nextScene;
    public override void EnterState()
    {
        base.EnterState();
        //Cursor.visible = false;
        //myStateMachine.UiController.EnterUiState("LoadState");
        _previousScene = SceneManager.GetActiveScene();
        GameState previousState = myStateMachine.previousState;

        if (previousState != null
            && (previousState.GetType() == typeof(StartState) || myStateMachine.previousState.GetType() == typeof(EndState)))
        {

        }
    }

    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
    public override void ExitState()
    {
        base.ExitState();
        //myStateMachine.UiController.ExitUiState("PlayingState");
    }
}
