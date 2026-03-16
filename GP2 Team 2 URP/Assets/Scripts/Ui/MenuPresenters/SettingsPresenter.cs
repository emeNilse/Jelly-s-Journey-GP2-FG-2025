using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsPresenter
{
    public Action ReturnToPause { get; set; }
    public Action ReturnToStart { get; set; }
    public Action Return {  get; set; }
    
    private Button returnButton;
    private Slider sfxSlider;
    private Slider musicSlider;
    public SettingsPresenter(VisualElement root)
    {
        
        returnButton = root.Q<Button>("Return");
        sfxSlider = root.Q<Slider>("SfxSlider");
        musicSlider = root.Q<Slider>("MusicSlider");
        
        //sfxSlider.dataSource = SoundManager.Instance;
        //musicSlider.dataSource = SoundManager.Instance;
        
        returnButton.clicked += () => ReturnButton();
        //returnButton.clicked += () => SoundManager.Instance.PlayMenuSelect();
    }
    private void ReturnButton()
    {
        Return?.Invoke();
    }
    

}
