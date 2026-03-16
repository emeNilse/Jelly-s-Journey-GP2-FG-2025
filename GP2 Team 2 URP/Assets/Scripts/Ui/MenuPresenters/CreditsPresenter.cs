using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class CreditsPresenter
{
    public Action Return { set => returnButton.clicked += value; }

    private Button returnButton;

    public CreditsPresenter(VisualElement root)
    {

        returnButton = root.Q<Button>("Return");
        //returnButton.clicked += () => SoundManager.Instance.PlayMenuSelect();

    }

}
