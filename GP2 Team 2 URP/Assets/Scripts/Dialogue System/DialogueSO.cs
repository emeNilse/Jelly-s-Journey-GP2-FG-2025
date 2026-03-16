using FMODUnity;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Scriptable Objects/DialogueSO")]
public class Dialogue : ScriptableObject
{
    [Tooltip("Unique identifier for this dialogue sequence")]
    public string dialogueName;

    [Tooltip("Array of all dialogue lines for this dialogue sequence")]
    public DialogueLine[] dialogueLines;
}

[System.Serializable]
public class DialogueLine
{
    [Tooltip("Name of the speaker for this line of dialogue")]
    public string speakerName;

    [Tooltip("Profile picture of the speaker (optional)")]
    public Sprite speakerProfilePicture;

    [Tooltip("The text to display for this line of dialogue")]
    [TextArea(3, 10)]
    public string dialogueText;

    [Tooltip("Voice-over FMOD event for this line (optional)")]
    public EventReference voiceOverEvent;

    [Tooltip("Optional: Duration (in seconds) to display this line. If set to 0, the Dialogue Manager will use the FMOD event's length if available; if neither is provided, a fallback default (set in the Dialogue Manager) is used.")]
    public float timing;

    [Tooltip("True if this line is spoken by the player; false for enemy")]
    public bool isPlayer;

    [Tooltip("Optional: Animation trigger name to activate when this line is played")]
    public string animationTrigger;
}