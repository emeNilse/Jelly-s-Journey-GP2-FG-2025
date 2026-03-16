using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EndMenuPresenter
{
    public Action OpenSettings { set => settingsButton.clicked += value; }
    public Action RestartLevel { set => restartButton.clicked += value; }
    public Action OpenCredits { set => creditsButton.clicked += value; }
    public Action EndRun { set => endRunButton.clicked += value; }
    public Action ExitGame { set => exitButton.clicked += value; }

    private Button restartButton;
    private Button settingsButton;
    private Button creditsButton;
    private Button exitButton;
    private Button endRunButton;

    public EndMenuPresenter(VisualElement root)
    {

        restartButton = root.Q<Button>("Restart");
        settingsButton = root.Q<Button>("Settings");
        creditsButton = root.Q<Button>("Credits");
        endRunButton = root.Q<Button>("EndRun");
        exitButton = root.Q<Button>("ExitGame");

        //restartButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //settingsButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //creditsButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //quitButton.clicked += () => SoundManager.Instance.PlayMenuSelect();

    }

}
