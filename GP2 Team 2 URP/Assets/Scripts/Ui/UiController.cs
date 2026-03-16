using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UiController : MonoBehaviour
{
    private GameManager _gameManager;
    public static UiController Instance;

    private VisualElement titleMenu;
    private VisualElement endMenu;
    private VisualElement pauseMenu;
    private VisualElement controlHintUi;
    private VisualElement settingsMenu;
    private VisualElement creditsMenu;

    public VisualElement CurrentMenuDisplayed { get; private set; }
    public VisualElement PreviousMenuDisplayed { get; private set; }
    private List<VisualElement> subMenus = new List<VisualElement>();

    private Dictionary<string, VisualElement> StateNameToVisualElement = new Dictionary<string, VisualElement>();

    public List<string> ActionNames = new List<string>();
    public List<Sprite> ControlSprites = new List<Sprite>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

    private void Initialize()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        titleMenu = root.Q("TitleMenu");
        pauseMenu = root.Q("PauseMenu");
        endMenu = root.Q("EndMenu");
        controlHintUi = root.Q("ControlsUi");
        settingsMenu = root.Q("SettingsMenu");
        creditsMenu = root.Q("CreditsMenu");
        subMenus.Add(settingsMenu);
        subMenus.Add(creditsMenu);

        StateNameToVisualElement.Add("StartState", titleMenu);
        StateNameToVisualElement.Add("PauseState", pauseMenu);
        StateNameToVisualElement.Add("EndState", endMenu);
        StateNameToVisualElement.Add("PlayingState", controlHintUi);
    }
    void Start()
    {
        _gameManager = GameManager.Instance;

        SetupTitleMenu();
        SetupPauseMenu();
        SetupEndMenu();
        SetupControlsHints();
        SetupSettingsMenu();
        //SetupCreditsMenu();
    }

    private void SetupControlsHints()
    {
        ControlsHintPresenter hintPresenter = new ControlsHintPresenter(controlHintUi, this);
    }

    private void SetupTitleMenu()
    {
        TitleMenuPresenter titlePresenter = new TitleMenuPresenter(titleMenu);

        titlePresenter.StartPlay = () =>
        {
            PlayerInputMapSwapper inputMapSwapper = _gameManager.player.GetComponent<PlayerInputMapSwapper>();
            if (inputMapSwapper == null)
            {
                _gameManager.SwitchState<PlayingState>();
            }
            else
            {
                inputMapSwapper.OnExitMenu();
            }
        };

        titlePresenter.OpenSettings = () =>
        {
            titleMenu.Display(false);
            PreviousMenuDisplayed = titleMenu;

            settingsMenu.Display(true);
            CurrentMenuDisplayed = settingsMenu;
        };

        titlePresenter.QuitPressed = () =>
        {
            Application.Quit();

         #if UNITY_EDITOR
              UnityEditor.EditorApplication.isPlaying = false;
         #endif

        };
    }
    private void SetupPauseMenu()
    {
        PauseMenuPresenter pausePresenter = new PauseMenuPresenter(pauseMenu);

        pausePresenter.UnPause = () =>
        {
            PlayerInputMapSwapper inputMapSwapper = _gameManager.player.GetComponent<PlayerInputMapSwapper>();
            if ( inputMapSwapper == null) 
            {
                _gameManager.SwitchState<PlayingState>();
            }
            else
            {
                inputMapSwapper.OnExitMenu();
            }
        };
        
        pausePresenter.OpenSettings = () =>
        {
            pauseMenu.Display(false);
            PreviousMenuDisplayed = pauseMenu;

            settingsMenu.Display(true);
            CurrentMenuDisplayed = settingsMenu;
        };

        pausePresenter.QuitPressed = () =>
        {
            Debug.Log("Quit pressed from Pause Menu. Temp functionality needs to be replaced by final behavior");
            _gameManager.SwitchState<StartState>();
        };
    }

    private void SetupEndMenu()
    {
        EndMenuPresenter endPresenter = new EndMenuPresenter(endMenu);

        endPresenter.RestartLevel = () =>
        {
            Debug.Log("Restart Level selected, temp just switch to startState, Need to set up real restart behavior");
            _gameManager.SwitchState<PlayingState>();
        };

        endPresenter.OpenSettings = () =>
        {
            endMenu.Display(false);
            PreviousMenuDisplayed = endMenu;

            settingsMenu.Display(true);
            CurrentMenuDisplayed = settingsMenu;
        };

        endPresenter.OpenCredits = () =>
        {
            Debug.Log("CREDITS BUTTON PRESSED, BUT CREDITS MENU NOT IMPLEMENTED YET");
            //endMenu.Display(false);
            //PreviousMenuDisplayed = endMenu;

            //creditsMenu.Display(true);
            //CurrentMenuDisplayed = creditsMenu;
        };

        endPresenter.EndRun = () =>
        {
            Debug.Log("Quit pressed from End of Run Menu. Temp functionality needs to be replaced by final behavior");
            _gameManager.SwitchState<StartState>();
        };

        endPresenter.ExitGame = () =>
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        };
    }

    private void SetupSettingsMenu()
    {
        SettingsPresenter settingsPresenter = new SettingsPresenter(settingsMenu);

        settingsPresenter.Return = () =>
        {
            VisualElement next = PreviousMenuDisplayed;
            settingsMenu.Display(false);
            PreviousMenuDisplayed = settingsMenu;

            next.Display(true);
            CurrentMenuDisplayed = next;
        };

    }

    private void SetupCreditsMenu()
    {
        CreditsPresenter creditsPresenter = new CreditsPresenter(creditsMenu);

        creditsPresenter.Return = () =>
        {
            VisualElement next = PreviousMenuDisplayed;
            creditsMenu.Display(false);
            PreviousMenuDisplayed = creditsMenu;

            next.Display(true);
            CurrentMenuDisplayed = next;
        };

    }

    public void EnterUiState(string state)
    {

        foreach(string key in StateNameToVisualElement.Keys)
        {
            if(state == key)
            {
                VisualElement uiToDisplay = StateNameToVisualElement[key];
                uiToDisplay.Display(true);
                CurrentMenuDisplayed = uiToDisplay;
            }
        }
    }

    private void HideDebugButton()
    {
        
    }
    public void ExitUiState(string state)
    {
        foreach (string key in StateNameToVisualElement.Keys)
        {
            if (state == key)
            {
                VisualElement uiToHide = StateNameToVisualElement[key];
                uiToHide.Display(false);
                PreviousMenuDisplayed = uiToHide;
            }
        }
    }

    public void GoToPreviousMenu()
    {
        VisualElement entering = PreviousMenuDisplayed;
        if (entering == null) entering = titleMenu;

        VisualElement leaving = CurrentMenuDisplayed;
        if (subMenus.Contains(leaving)) 
        {
            // I think this is only going to be used for esc key toggling back to the previous menu from a sub menu like settings or credits
            leaving.Display(false);
            PreviousMenuDisplayed = leaving;

            entering.Display(true);
            CurrentMenuDisplayed = entering;
        } 
    }
}
