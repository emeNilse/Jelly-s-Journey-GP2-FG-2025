using EncounterSystem;
using FMODUnity;
using RoomSystem;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Turner added this 2/11/2025
public class BgmManager : MonoBehaviour
{
    

    [SerializeField] private string bgmEventPath;
    public string FinalFightRoomName = "SampleScene 3";
    private FMOD.Studio.EventInstance bgmInstance;
    private MusicState _currentMusicState;
    private SpawnManager _currentSpawnManager;
    // This lets you change the bgm from the inspector
    public MusicState CurrentMusicState { 
        get {
            return _currentMusicState;
        } 
        
        set {
            if(_currentMusicState == value) return;
            switch (value)
            {
                case MusicState.Main:
                    ChangeBgmToMain();
                    break;
                case MusicState.Battle:
                    ChangeBgmToBattle();
                    break;
                case MusicState.Boss:
                    ChangeBgmToBoss();
                    break;
                default:
                    Debug.LogError("Invalid MusicState");
                    ChangeBgmToMain();
                    break;
            }
        }
    }

    // Create an instance of the bgm event
    private void Awake()
    {
        bgmInstance = RuntimeManager.CreateInstance(bgmEventPath);
    }

    // Start the bgm instance and set the MusicState to Main at the start of the game.
    private void Start()
    {
        // Set the bgm paramter MusicState to "Main" 
        ChangeBgmToMain();
        bgmInstance.start();
        RoomNavigator.Instance.RoomLoadComplete.AddListener(OnRoomLoaded);
    }

    private void OnRoomLoaded(Room room)
    {
        _currentSpawnManager = FindFirstObjectByType<SpawnManager>();
        if (_currentSpawnManager != null)
        {
            if (room.SceneName == FinalFightRoomName)
            {
                _currentSpawnManager.OnEncounterStart.AddListener(ChangeBgmToBoss);
                _currentSpawnManager.OnEncounterEnd.AddListener(ChangeBgmToMain);
            }
            else
            {
                _currentSpawnManager.OnEncounterStart.AddListener(ChangeBgmToBattle);
                _currentSpawnManager.OnEncounterEnd.AddListener(ChangeBgmToMain);
            }
        }
    }

    // Changing the BGM to the Main, Battle, or Boss state functions
    public void ChangeBgmToMain()
    {
        RuntimeManager.StudioSystem.setParameterByName("MusicState", (float)MusicState.Main);
        _currentMusicState = MusicState.Main;
    }

    public void ChangeBgmToBattle()
    {
        RuntimeManager.StudioSystem.setParameterByName("MusicState", (float)MusicState.Battle);
        _currentMusicState = MusicState.Battle;
    }

    public void ChangeBgmToBoss()
    {
        RuntimeManager.StudioSystem.setParameterByName("MusicState", (float)MusicState.Boss);
        _currentMusicState = MusicState.Boss;
    }

    // Release and clean up the instance when the object is destroyed
    private void OnDestroy()
    {
        bgmInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        bgmInstance.release();
    }

}


    public enum MusicState
    {
        Main,
        Battle,
        Boss
    }
