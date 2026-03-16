using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class PlayerInputMapSwapper : MonoBehaviour
{
    public PlayerInput Input;
    InputActionMap _gameplayMap;
    InputActionMap _uiMap;
    UiMenuController _uiMenuController;
    //private static PlayerInputMapSwapper instance;
    [Header("Debug Stuff")]
    public bool PrintDebugLogs = false;
    private void Awake()
    {
        //if(instance == null)
        //{
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}

    }
    private void Start()
    {
        SetInputMaps();
        _uiMenuController = GameObject.Find("Menu Canvas").GetComponent<UiMenuController>();
    }
    private void SetInputMaps()
    {
        Input = GetComponent<PlayerInput>();
        _gameplayMap = Input.actions.actionMaps[0];
        _uiMap = Input.actions.actionMaps[1];
    }
    void OnMenu()
    {
        GameManager gameManager = GameManager.Instance;
        if(gameManager.CurrentState is PlayingState)
        {
            EnableUiInputs();
            gameManager.switchState<PauseState>();
        }
    }
    public void OnExitMenu()
    {
        _uiMenuController.HandleOnMenuExit();
    }


    public void EnableGameplayInputs()
    {
        if (_uiMap == null || _gameplayMap == null) SetInputMaps();
        if (PrintDebugLogs) Debug.Log($"{this} is trying to enable only gameplay");

        _uiMap.Disable();
        _gameplayMap.Enable();
        Input.currentActionMap = _gameplayMap;
        if (PrintDebugLogs) Debug.Log($"input current actionmap is {Input.currentActionMap?.name}");
        if (PrintDebugLogs) Debug.Log($"{this} has gameplaymap that is enabled = {_gameplayMap.enabled}, and ui map that is enabled = {_uiMap.enabled}");
        //Debug.Log($"just tried to switch to player action map and current action map is {_input.currentActionMap.name}");
    }
    public void EnableUiInputs()
    {
        if (_uiMap == null || _gameplayMap == null) SetInputMaps();
        if (PrintDebugLogs) Debug.Log($"{this} is trying to enable only ui map");
        _gameplayMap.Disable();
        _uiMap.Enable();
        Input.currentActionMap = _uiMap;
        if (PrintDebugLogs) Debug.Log($"input current actionmap is {Input.currentActionMap?.name}");
        if (PrintDebugLogs) Debug.Log($"{this} has gameplaymap that is enabled = {_gameplayMap.enabled}, and ui map that is enabled = {_uiMap.enabled}");
        // Debug.Log($"just tried to switch to UI action map and current action map is {_input.currentActionMap.name}");
    }

    public void ToggleInputsEnabled(bool isEnable = true)
    {
        if (isEnable)
        {
            GameState currentState = GameManager.Instance.CurrentState as GameState;
            if (currentState is PlayingState)
            {
                EnableGameplayInputs();
            }
            else
            {
                EnableUiInputs();
            }
        }
        else
        {
            if (_uiMap == null || _gameplayMap == null) SetInputMaps();
            _uiMap.Disable();
            _gameplayMap.Disable();
        }

    }

}
