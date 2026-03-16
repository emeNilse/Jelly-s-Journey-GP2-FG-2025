using UnityEngine;
using FMOD.Studio;
using FMODUnity;

// This should only run in edit mode not when built
public class DebugAudioSettings : MonoBehaviour
{
    // This class should do nothing when built
    // It is only for debugging purposes
    // It should not be included in the build
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Voice;
    FMOD.Studio.Bus UI;
    FMOD.Studio.Bus Ambience;
    FMOD.Studio.Bus Master;


    // Fields to be able to change the audio in the editor
    [Range(0.0f, 1.0f)] [SerializeField] private float masterVolume = 1f;
    [Range(0.0f, 1.0f)] [SerializeField] private float sfxVolume = 1f;
    [Range(0.0f, 1.0f)] [SerializeField] private float musicVolume = 1f;
    [Range(0.0f, 1.0f)] [SerializeField] private float voiceVolume = 1f;
    [Range(0.0f, 1.0f)] [SerializeField] private float uiVolume = 1f;
    [Range(0.0f, 1.0f)] [SerializeField] private float ambienceVolume = 1f;

    #if UNITY_EDITOR
    void Awake() {
        Debug.Log("DebugAudioSettings is enabled");

        Music = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        Voice = FMODUnity.RuntimeManager.GetBus("bus:/Master/Voice");
        UI = FMODUnity.RuntimeManager.GetBus("bus:/Master/UI");
        Ambience = FMODUnity.RuntimeManager.GetBus("bus:/Master/Amb");
        Master = FMODUnity.RuntimeManager.GetBus("Bus:/Master");
    }

    // Update is called once per frame
    void Update()
    {
        Music.setVolume(musicVolume);
        SFX.setVolume(sfxVolume);
        Voice.setVolume(voiceVolume);
        UI.setVolume(uiVolume);
        Ambience.setVolume(ambienceVolume);
        Master.setVolume(masterVolume);
    }

    #else
        void OnEnable()
        {
            Debug.Log("DebugAudioSettings should not be enabled in build");
            Destroy(this);
        }
    #endif
}