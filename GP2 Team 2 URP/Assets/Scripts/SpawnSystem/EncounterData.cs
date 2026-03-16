using System.Collections.Generic;
using UnityEngine;
namespace EncounterSystem
{
    //[CreateAssetMenu(fileName = "EncounterData", menuName = "Scriptable Objects/EncounterData")]
    [System.Serializable]
    public class EncounterData //: ScriptableObject
    {
        public bool RespawnsOnBacktrack = false;
        [Header("Wave Settings")]
        public bool isSpawnImmediate = true;
        public float MinSecondsBetweenWaves;
        public int EnemiesRemainingWaveTrigger;
        public List<WaveData> Waves = new List<WaveData>();

        [Header("Enemy Settings")]
        public List<GameObject> EnemyPrefabs = new List<GameObject>();
    }
    [System.Serializable]
    public class WaveData
    {
        public float SecondsBtwSpawns = 1;
        public int NumberRanged = 2;
        public int NumberMelee = 3;
    }
}