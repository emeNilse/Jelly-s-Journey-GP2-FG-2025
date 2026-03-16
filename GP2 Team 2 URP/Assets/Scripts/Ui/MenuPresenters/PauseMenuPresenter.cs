using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuPresenter
{
    public Action OpenSettings { set => settingsButton.clicked += value; }
    public Action UnPause { set => playButton.clicked += value; }
    public Action QuitPressed { set => quitButton.clicked += value; }

    private Button playButton;
    private Button settingsButton;
    private Button quitButton;

    private Button debugEndStateButton;
    public PauseMenuPresenter(VisualElement root)
    {
        playButton = root.Q<Button>("Resume");

        settingsButton = root.Q<Button>("Settings");
        quitButton = root.Q<Button>("Quit");

        debugEndStateButton = root.Q<Button>("EndStateButton");
        debugEndStateButton.clicked += () => GameManager.Instance.switchState<EndState>();
        //playButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //settingsButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //quitButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
    }
}
