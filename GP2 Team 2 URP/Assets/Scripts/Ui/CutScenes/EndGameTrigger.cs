using EncounterSystem;
using UnityEngine;
using System.Collections.Generic;

public class EndGameTrigger : MonoBehaviour
{
    public SpawnManager spawnManager;
    [SerializeField] private Animator _alchemistAnimator;
    [SerializeField] private string _triggeredAnimationName;
    [SerializeField] private float _secondsToWaitBeforeOutroPlays;
    public List<GameObject> ObjectsToEnableOnEndUnlock = new List<GameObject>();
    
    private BoxCollider _triggerVolume;
    private CutScene _cutScene;
    private void OnEnable()
    {
        spawnManager.OnEncounterStart.AddListener(OnEncounterStart);
        spawnManager.OnEncounterEnd.AddListener(OnEncounterEnd);
        _triggerVolume = GetComponent<BoxCollider>();
        foreach (GameObject obj in ObjectsToEnableOnEndUnlock)
        {
            obj.SetActive(false);
        }
    }
    private void Start()
    {
        //_triggerVolume.enabled = false;
        _cutScene = GameManager.Instance.GetComponentInChildren<CutScene>();
        Debug.Log($"end game trigger Found {_cutScene} on start");
    }

    private void OnDisable()
    {
        spawnManager.OnEncounterStart.RemoveListener(OnEncounterStart);
        spawnManager.OnEncounterEnd.RemoveListener(OnEncounterEnd);
    }

    private void OnEncounterStart()
    {
        Debug.Log("end game trigger heard the encounter START call");
        _triggerVolume.enabled = false;
    }

    private void OnEncounterEnd()
    {
        Debug.Log("end game trigger heard the encounter END call");
        foreach(GameObject obj in ObjectsToEnableOnEndUnlock)
        {
            obj.SetActive(true);
        }
        _triggerVolume.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("something entered the end game trigger");
        if (other.CompareTag("Player"))
        {
            if(_alchemistAnimator == null)
            {
                OnAnimationEnd();
            }
            else
            {
                _alchemistAnimator.SetTrigger("StartTalking");
                StartCoroutine(WaitTime());
            }
            Debug.Log("PLAYER ENTERED THE END GAME TRIGGER");
            _triggerVolume.enabled = false;
        }
    }

    private System.Collections.IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_secondsToWaitBeforeOutroPlays);
        OnAnimationEnd();
    }
    public void OnAnimationEnd()
    {
        _cutScene.StartOutro();

    }

}
