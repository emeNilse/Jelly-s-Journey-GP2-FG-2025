using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using RoomSystem;

namespace EncounterSystem
{
    public class SpawnManager : MonoBehaviour
    {
        // is this a thing that lives on the game manager, or on the room?
        private PlayingState _updateState;
        private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
       // private Dictionary<AllowedEnemyType, System.Type> _enemyTypeLookup = new Dictionary<AllowedEnemyType, System.Type>();
        public EncounterData CurrentEncounterData = new EncounterData();
        [HideInInspector] public EnemyPool CurrentEnemyPool;
        private bool _isSpawning;
        private float _timeSinceLastSpawn = 0;
        private int _currentWave = 0;
        private bool _waveEnded = false;
        private bool _encounterEnded = false;
        private bool _isBacktracking = false;

        public UnityEvent OnEncounterStart;
        public UnityEvent<int> OnWaveStart;
        public UnityEvent<int> OnWaveEnd;
        public UnityEvent OnEncounterEnd;

        [Header("DEBUG LOGS")]
        public bool PrintDebugLogText = false;
        [Header("DEBUG 'BUTTONS'")]
        [SerializeField] private bool _killAll = false;
        [SerializeField] private bool _resetWaves = false;
        void Start()
        {
           // InitializeEnemyTypeLookup();
            CurrentEnemyPool = this.gameObject.AddComponent<EnemyPool>();
            CurrentEnemyPool.InitializePool(CurrentEncounterData.EnemyPrefabs);
            SubscribeToPlayState();

            _timeSinceLastSpawn = CurrentEncounterData.isSpawnImmediate ? CurrentEncounterData.MinSecondsBetweenWaves : 0;
            RoomNavigator navigator = GameManager.Instance.GetComponent<RoomNavigator>();
            if(navigator != null )
            {
                //navigator.UpdateRoomEncounter(CurrentEncounterData.RespawnsOnBacktrack);
                _isBacktracking = navigator.IsCurrentRoomBacktracking;
                if (PrintDebugLogText) Debug.Log($"backtracking is {_isBacktracking}");
                //navigator.RoomLoadComplete.AddListener(OnRoomLoaded);
            }
        }
        private void OnDisable()
        {
            StopAllCoroutines();
        }
        private void OnRoomLoaded(Room room)
        {
            
        }
        void ManagedUpdate()
        {

            if(CurrentEncounterData != null
                && CurrentEncounterData.Waves.Count > 0
                && CurrentEnemyPool != null)
            {
                if (!CurrentEncounterData.RespawnsOnBacktrack && _isBacktracking)
                {
                    return;
                }


                if (CurrentEnemyPool.PrintDebugLogText != PrintDebugLogText)
                {
                    CurrentEnemyPool.PrintDebugLogText = PrintDebugLogText;
                }

                if (!_isSpawning
                && _currentWave < CurrentEncounterData.Waves.Count
                && CurrentEnemyPool.SpawnCount <= CurrentEncounterData.EnemiesRemainingWaveTrigger
                && _timeSinceLastSpawn >= CurrentEncounterData.MinSecondsBetweenWaves)
                {
                    _isSpawning = true;
                    if(PrintDebugLogText) Debug.Log($"------ BEGINNING WAVE SPAWN ------ SpawnCount is: {CurrentEnemyPool.SpawnCount}, current wave is: {_currentWave}");

                    if (_currentWave == 0 && OnEncounterStart != null)
                    {
                        OnEncounterStart.Invoke();
                        if (PrintDebugLogText) Debug.Log("Invoked OnEncounterStart");
                    }
                    
                    StartCoroutine(SpawnWave());
                }
                _timeSinceLastSpawn += Time.deltaTime;

                if (_killAll)
                {
                    StopAllCoroutines();
                    CurrentEnemyPool.KillAll();
                    _killAll = false;
                }

                if (!_isSpawning
                    && CurrentEnemyPool.SpawnCount == 0 
                    && !_encounterEnded)
                {
                    if (PrintDebugLogText) Debug.Log($"Entered end of wave state to check for end of encounter or send end of wave event");
                    if (_waveEnded && _currentWave == CurrentEncounterData.Waves.Count)
                    {
                        _encounterEnded = true;
                        if(OnEncounterEnd != null) OnEncounterEnd.Invoke();
                        if (PrintDebugLogText) Debug.Log("Invoked OnEncounterEnd");
                    }
                    if (!_waveEnded) 
                    {
                        _waveEnded = true;
                        OnWaveEnd.Invoke(_currentWave);
                        if (PrintDebugLogText) Debug.Log($"Invoked OnWaveEnd for wave {_currentWave}");
                    }
                }
                if (_resetWaves)
                {
                    StopAllCoroutines();
                    CurrentEnemyPool.ResetPool();
                    _currentWave = 0;
                    _resetWaves = false;
                    _encounterEnded = false;
                }
            }
            

        }
        private IEnumerator SpawnWave()
        {
            if (PrintDebugLogText) Debug.Log($"----------- SPAWN WAVE COROUTINE FIRST LINE -----------");
            _waveEnded = false;
            if (OnWaveStart != null)
            {
                OnWaveStart.Invoke(_currentWave);
                if (PrintDebugLogText) Debug.Log($"Invoked OnWaveStart for wave {_currentWave}");
            }
            WaveData data = CurrentEncounterData.Waves[_currentWave];
            int enemiesPerWave = data.NumberMelee + data.NumberRanged;
            int meleeCount = 0;
            int rangedCount = 0;
            int loopcount = 1;

            while((meleeCount + rangedCount) < enemiesPerWave)
            {
                if (PrintDebugLogText) Debug.Log($"----------- PASS #{loopcount} OF SPAWN LOOP FOR WAVE #{_currentWave} -----------");
                if (PrintDebugLogText) Debug.Log($"SpawnManager.SpawnWave(): ranged enemies spawned {rangedCount}, melee enemies spawned{meleeCount}");
                foreach (SpawnPoint point in _spawnPoints)
                {
                    AllowedEnemyType spawnType = point.AllowedEnemyType;
                    bool spawnSuccess = false;
                    switch (spawnType)
                    {
                        case AllowedEnemyType.Any:
                            bool coinFlip = UnityEngine.Random.Range(0, 100) >= 50;
                            if(meleeCount < data.NumberMelee 
                                && (rangedCount == data.NumberRanged || rangedCount > meleeCount))
                            {
                                spawnSuccess = SpawnByType(point, typeof(MeleeEnemy), data.NumberMelee, meleeCount);
                                if (spawnSuccess) meleeCount++;
                            }
                            else if (rangedCount < data.NumberRanged 
                                && (meleeCount == data.NumberMelee || meleeCount > rangedCount))
                            {
                                spawnSuccess = SpawnByType(point, typeof(RangedEnemy), data.NumberRanged, rangedCount);
                                if (spawnSuccess) rangedCount++;
                            } 
                            else if (coinFlip)
                            {
                                spawnSuccess = SpawnByType(point, typeof(MeleeEnemy), data.NumberMelee, meleeCount);
                                if (spawnSuccess) meleeCount++;
                            }
                            else
                            {
                                spawnSuccess = SpawnByType(point, typeof(RangedEnemy), data.NumberRanged, rangedCount);
                                if (spawnSuccess) rangedCount++;
                            }
                            break;

                        case AllowedEnemyType.Melee:
                            spawnSuccess = SpawnByType(point, typeof(MeleeEnemy), data.NumberMelee, meleeCount);
                            if (spawnSuccess) meleeCount++;
                            break;

                        case AllowedEnemyType.Ranged:
                            spawnSuccess = SpawnByType(point, typeof(RangedEnemy), data.NumberRanged, rangedCount);
                            if (spawnSuccess) rangedCount++;
                            break;

                        default:
                            break;
                    }
                    
                    yield return new WaitForSeconds(data.SecondsBtwSpawns);
                }
                loopcount++;
            }
            
            if (PrintDebugLogText) Debug.Log($"----------------------- Completed spawning wave #{_currentWave} -----------------------");
            _currentWave++;
            _isSpawning = false;
        }

        private bool SpawnByType(SpawnPoint point, System.Type allowedType, int maxToSpawn, int typeSpawnedCount)
        {
            if(typeSpawnedCount < maxToSpawn)
            {
                if (PrintDebugLogText) Debug.Log($"SpawnManager is asking to spawn {allowedType} on a spawnpoint of {point.AllowedEnemyType}.");
                PooledObject instance = CurrentEnemyPool.SpawnInstance(point.GetPosition(), allowedType, point); 
                _timeSinceLastSpawn = 0;
                return true;
            }
            else
            {
                if (PrintDebugLogText) Debug.Log($"will not ask to spawn {allowedType} on a spawnpoint of {point.AllowedEnemyType}, because max number of {allowedType} already are spawned");
                return false;
            }
        }


        void ManagedFixedUpdate() { }

        public void RegisterSpawnPoint(SpawnPoint point)
        {
            _spawnPoints.Add(point);
        }

        public void UnregisterSpawnPoint(SpawnPoint point)
        {
            _spawnPoints.Remove(point);
        }

        private void SubscribeToPlayState()
        {
            _updateState = GameManager.Instance.GetState<PlayingState>();
            if (_updateState != null)
            {
                _updateState.StateUpdate.AddListener(ManagedUpdate);
                _updateState.StateFixedUpdate.AddListener(ManagedFixedUpdate);
            }
            else
            {
                if (PrintDebugLogText) Debug.Log("tried to add listener but myUpdateState == null");
            }
        }

        private void UnsubscribeToPlayState()
        {
            if (_updateState != null)
            {
                _updateState.StateUpdate.RemoveListener(ManagedUpdate);
                _updateState.StateFixedUpdate.RemoveListener(ManagedFixedUpdate);
            }
            else
            {
                if (PrintDebugLogText) Debug.Log("tried to remove listeners but myUpdateState == null");
            }
        }
    }
}