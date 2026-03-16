using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleMenuPresenter
{
    public Action OpenSettings { set => settingsButton.clicked += value; }
    public Action StartPlay { set => startButton.clicked += value; }
    public Action QuitPressed { set => quitButton.clicked += value; }

    private Button startButton;
    private Button settingsButton;
    private Button quitButton;

    public TitleMenuPresenter(VisualElement root)
    {

        startButton = root.Q<Button>("Start");
        settingsButton = root.Q<Button>("Settings");
        quitButton = root.Q<Button>("Quit");

        //startButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //settingsButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
        //quitButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
    }
}
