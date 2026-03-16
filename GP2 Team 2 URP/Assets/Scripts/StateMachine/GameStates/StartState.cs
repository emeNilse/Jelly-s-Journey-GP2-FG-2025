using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : GameState
{
    private string _sceneName;
    public string SceneName { get { return _sceneName; } }

    public GameObject playerUI;
    public override void EnterState()
    {
        base.EnterState();
        PlayerInputMapSwapper inputMapSwapper = myStateMachine.player.GetComponent<PlayerInputMapSwapper>();
        if (inputMapSwapper != null)
        {
            inputMapSwapper.EnableUiInputs();
        }
        

        //myStateMachine.UiController.EnterUiState("StartState");
        //if(_sceneName == "" && GameManager.Instance.TitleSceneName != "")
        //{
        //    _sceneName = GameManager.Instance.TitleSceneName;
        //}

        GameState previousState = myStateMachine.previousState;
        //Debug.Log($"my state machine is {myStateMachine} and my UiController is {myStateMachine.UiController}, and my state machine's previous state is {previousState}");
        if (previousState != null 
            && previousState.GetType() != typeof(StartState))
        {
            Scene titleScene = SceneManager.GetSceneByName(_sceneName);
            if (titleScene.name != null)
            {
                SceneManager.LoadScene(_sceneName);
            }
        }
        Time.timeScale = 0f;

    }

    public override void UpdateState()
    {
        base.UpdateState();
       /* if (myStateMachine.UiController.CurrentMenuDisplayed.name == "SettingsMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                myStateMachine.UiController.GoToPreviousMenu();
            }
        }
        Debug.Log($"my state machine is {myStateMachine}, uiController is {myStateMachine.UiController}, and current menu displayed is {myStateMachine.UiController.CurrentMenuDisplayed}");
       */
    }

    public override void ExitState()
    {
        base.ExitState();
        //myStateMachine.UiController.ExitUiState("StartState");
        //Time.timeScale = 1f;
    }

}
