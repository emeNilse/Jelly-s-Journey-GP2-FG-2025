using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialogueUIPanel;
    public TMP_Text dialogueText;
    [Tooltip("Optional: Animation player to trigger animations for dialogue lines")]
    public Animator animator; // Animation player

    [Header("Default Portraits")]
    public Sprite defaultPlayerPortrait;
    public Sprite defaultEnemyPortrait;

    [Header("Portrait Containers")]
    public Image leftPortrait;
    public Image rightPortrait;

    [Header("Effects")]
    [Range(0, 1)]
    public float typingDuration = 0.5f;

    [Header("Dialogues")]
    public Dialogue[] dialogues;

    [Header("Timing")]
    public float defaultDialogueLineDuration = 3.0f;

    [Header("Input")]
    public InputActionReference dialogueSkipAction;

    [Header("Skip Settings")]
    [Tooltip("Time (in seconds) to wait between accepted skip inputs")]
    public float skipCooldownDuration = 0.25f;

    [Header("Dialogue Lifecycle Events")]
    public UnityEvent OnDialogueStart;
    public UnityEvent OnDialogueLineChanged;
    public UnityEvent OnDialogueEnd;

    private Dictionary<string, Dialogue> dialogueLookup;
    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    private Dialogue currentDialogue;
    private int currentLineIndex;
    private EventInstance currentVoiceEvent;

    // Single coroutine for displaying a line.
    private Coroutine displayLineCoroutine;
    private bool skipRequested = false;
    private bool dialogueActive = false;

    // Flag to know if the current line is fully displayed.
    private bool lineFullyDisplayed = false;

    // For skip cooldown.
    private float lastSkipTime = -Mathf.Infinity;

    private void Awake()
    {
        dialogueLookup = new Dictionary<string, Dialogue>();
        foreach (var dialogue in dialogues)
        {
            if (!dialogueLookup.ContainsKey(dialogue.dialogueName))
                dialogueLookup.Add(dialogue.dialogueName, dialogue);
            else
                Debug.LogWarning($"Duplicate dialogue name found: {dialogue.dialogueName}");
        }
    }

    private void OnEnable()
    {
        if (dialogueSkipAction != null)
            dialogueSkipAction.action.performed += OnDialogueSkip;
    }

    private void OnDisable()
    {
        if (dialogueSkipAction != null)
            dialogueSkipAction.action.performed -= OnDialogueSkip;
    }

    private void Start()
    {
        dialogueUIPanel.SetActive(false);
    }

    public void StartDialogueByName(string dialogueName)
    {
        if (dialogueLookup.TryGetValue(dialogueName, out Dialogue dialogue))
            StartDialogue(dialogue);
        else
            Debug.LogWarning($"Dialogue '{dialogueName}' not found.");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (dialogueActive)
        {
            dialogueQueue.Enqueue(dialogue);
            return;
        }
        BeginDialogue(dialogue);
    }

    private void BeginDialogue(Dialogue dialogue)
    {
        dialogueActive = true;
        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialogueUIPanel.SetActive(true);
        OnDialogueStart?.Invoke();
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        // Disable both portriats by default.
        leftPortrait.gameObject.SetActive(false);
        rightPortrait.gameObject.SetActive(false);
        
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
            displayLineCoroutine = null;
        }

        if (currentDialogue == null || currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        // Reset flag for new line.
        lineFullyDisplayed = false;
        skipRequested = false;

        DialogueLine line = currentDialogue.dialogueLines[currentLineIndex++];
        OnDialogueLineChanged?.Invoke();
        
        Image profileImage = line.isPlayer ? leftPortrait : rightPortrait;
        // Set portrait: use line's portrait if available, else fallback to default.
        if (line.speakerProfilePicture != null)
        {
            profileImage.sprite = line.speakerProfilePicture;
            profileImage.gameObject.SetActive(true);
        }
        else
        {
            if (line.isPlayer)
            {
                if (defaultPlayerPortrait != null)
                {
                    profileImage.sprite = defaultPlayerPortrait;
                    profileImage.gameObject.SetActive(true);
                }
                else
                {
                    profileImage.gameObject.SetActive(false);
                }
            }
            else
            {
                if (defaultEnemyPortrait != null)
                {
                    profileImage.sprite = defaultEnemyPortrait;
                    profileImage.gameObject.SetActive(true);
                }
                else
                {
                    profileImage.gameObject.SetActive(false);
                }
            }
        }

        // Trigger animation if defined.
        if (!string.IsNullOrEmpty(line.animationTrigger) && animator != null)
        {
            animator.SetTrigger(line.animationTrigger);
        }

        displayLineCoroutine = StartCoroutine(DisplayLineCoroutine(line));
    }

    private IEnumerator DisplayLineCoroutine(DialogueLine line)
    {
        dialogueText.text = "";
        float lineDelay = line.timing > 0 ? line.timing : defaultDialogueLineDuration;

        // Start voice-over if provided.
        if (!line.voiceOverEvent.IsNull)
        {
            if (currentVoiceEvent.isValid())
            {
                currentVoiceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                currentVoiceEvent.release();
            }
            currentVoiceEvent = RuntimeManager.CreateInstance(line.voiceOverEvent);
            currentVoiceEvent.start();

            if (line.timing <= 0 && currentVoiceEvent.isValid() &&
                currentVoiceEvent.getDescription(out EventDescription description) == FMOD.RESULT.OK)
            {
                description.getLength(out int lengthMs);
                lineDelay = lengthMs / 1000f;
            }
        }

        // Typewriter effect.
        float typingSpeed = line.dialogueText.Length > 0 ? (defaultDialogueLineDuration * typingDuration) / line.dialogueText.Length : 0f;
        float accumulatedTime = 0f;
        int currentCharIndex = 0;
        float elapsedTime = 0f;

        // Type out characters until full line is shown.
        while (currentCharIndex < line.dialogueText.Length)
        {
            if (skipRequested)
            {
                // First skip: finish typing immediately.
                dialogueText.text = line.dialogueText;
                break;
            }

            float deltaTime = Time.unscaledDeltaTime;
            accumulatedTime += deltaTime;
            elapsedTime += deltaTime;

            while (accumulatedTime >= typingSpeed && currentCharIndex < line.dialogueText.Length)
            {
                dialogueText.text += line.dialogueText[currentCharIndex];
                currentCharIndex++;
                accumulatedTime -= typingSpeed;
            }
            yield return null;
        }

        // Mark that the text is now fully displayed.
        dialogueText.text = line.dialogueText;
        lineFullyDisplayed = true;

        // Wait out any remaining delay unless skip is requested.
        float remainingDelay = Mathf.Max(0, lineDelay - elapsedTime);
        float waitTimer = 0f;
        while (waitTimer < remainingDelay)
        {
            if (skipRequested)
                break;
            waitTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Reset skip flag and proceed.
        skipRequested = false;
        DisplayNextLine();
    }

    public void EndDialogue()
    {
        dialogueUIPanel.SetActive(false);
        dialogueActive = false;

        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
            displayLineCoroutine = null;
        }

        if (currentVoiceEvent.isValid())
        {
            currentVoiceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentVoiceEvent.release();
        }

        OnDialogueEnd?.Invoke();
        currentDialogue = null;

        if (dialogueQueue.Count > 0)
            BeginDialogue(dialogueQueue.Dequeue());
        
        leftPortrait.gameObject.SetActive(false);
        rightPortrait.gameObject.SetActive(false);
    }

    private void OnDialogueSkip(InputAction.CallbackContext context)
    {
        if (!dialogueActive || !dialogueUIPanel.activeSelf)
            return;

        // Enforce skip cooldown.
        if (Time.unscaledTime - lastSkipTime < skipCooldownDuration)
            return;
        lastSkipTime = Time.unscaledTime;

        // If text is not yet fully displayed, this skip finishes the typewriter effect.
        if (!lineFullyDisplayed)
        {
            skipRequested = true;
        }
        else
        {
            // If text is fully displayed, a skip will cancel the voice-over and move on.
            skipRequested = true;
            if (currentVoiceEvent.isValid())
            {
                currentVoiceEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                currentVoiceEvent.release();
            }
        }
    }
}