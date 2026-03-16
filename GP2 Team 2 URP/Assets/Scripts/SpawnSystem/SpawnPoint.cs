using UnityEngine;
//using UnityEngine.Events;
namespace EncounterSystem
{
    public enum AllowedEnemyType { Any, Melee, Ranged }
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        protected AllowedEnemyType _allowedEnemyType;
        public AllowedEnemyType AllowedEnemyType { get { return _allowedEnemyType; } }
        
        //public UnityEvent<Vector3> OnEnemySpawn;

        void Awake()
        {
            SpawnManager spawnManager = FindAnyObjectByType<SpawnManager>();
            if (spawnManager != null )
            {
                spawnManager.RegisterSpawnPoint( this );
            }
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}