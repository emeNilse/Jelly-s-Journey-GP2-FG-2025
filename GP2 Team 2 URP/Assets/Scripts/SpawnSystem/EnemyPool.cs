using System.Collections.Generic;
using UnityEngine;
namespace EncounterSystem
{
    public class EnemyPool : ObjectPool
    {
        public bool PrintDebugLogText = false;
        public override void ReturnToPool(PooledObject instance)
        {

            if (_activePool.Contains(instance))
            {
                //may need to disable colliders here if having weird physics with objects set to inactive
                instance.OnReturnToPool.RemoveAllListeners();
                instance.gameObject.SetActive(false);
                instance.transform.SetParent(transform);
                _activePool.Remove(instance);
                _inactivePool.Add(instance);

                SpawnCount--;
            }
            else
            {
                if (PrintDebugLogText) Debug.LogError(this.name + " was told to that " + instance + " needs to return to pool, but that enemy is not in this manager's pool.");
            }
        }
        
        public override PooledObject SpawnInstance(Vector3 spawnPosition)
        {
            PooledObject currentInstance = GetMatchingInstance(typeof(Enemy));
            if (currentInstance != null)
            {
                _inactivePool.Remove(currentInstance);
            }
            else
            {
                //if inactive pool is empty or has no matches then current instance is still null and we make a new enemy
                currentInstance = CreateNewInstance(typeof(Enemy));
            }
            currentInstance.transform.position = spawnPosition;
            currentInstance.gameObject.SetActive(true);
            //currentInstance.transform.SetParent(transform);
            currentInstance.OnReturnToPool.AddListener(ReturnToPool);

            _activePool.Add(currentInstance);
            SpawnCount++;
            return currentInstance;

        }
        public PooledObject SpawnInstance(Vector3 spawnPosition, System.Type allowedType, SpawnPoint spawnPoint = null)
        {
            PooledObject currentInstance = null;
            if (_inactivePool.Count > 0)
            {
                currentInstance = GetMatchingInstance(allowedType);
                _inactivePool.Remove(currentInstance);
            }
            else
            {
                //if inactive pool is empty or has no matches then current instance is still null and we make a new enemy
                currentInstance = CreateNewInstance(allowedType);
            }

            currentInstance.transform.position = spawnPosition;
            currentInstance.gameObject.SetActive(true);
            if(spawnPoint != null)
            {
                currentInstance.transform.SetParent(spawnPoint.transform,true);
            }
            currentInstance.OnReturnToPool.AddListener(ReturnToPool);

            _activePool.Add(currentInstance);
            SpawnCount++;
            if (PrintDebugLogText) Debug.Log($"----- EnemyPool.SpawnInstance(): SpawnCount is: {SpawnCount}, current instance is: {currentInstance}");
            return currentInstance;

        }
        private PooledObject CreateNewInstance(System.Type allowedType )
        {
            GameObject matchingEnemy = null;
            PooledObject instance = null;
            foreach (GameObject prefab in Prefabs)
            {
                System.Type enemyType = prefab.GetComponent<Enemy>().GetType();
                if (allowedType == typeof(Enemy) || enemyType == allowedType)
                {
                    matchingEnemy = Instantiate(prefab, transform);
                    matchingEnemy.AddComponent<PooledObject>();
                    instance = matchingEnemy.GetComponent<PooledObject>();
                    if (instance == null) Debug.LogError($"prefab {prefab} has no pooledObject Component");
                    break;
                }
            }
            //Debug.Log($"allowed type is: {allowedType}, new enemy instance is: {instance}");
            return instance;
        }
        private PooledObject GetMatchingInstance(System.Type allowedType)
        {
            PooledObject matchingEnemy = null;
            foreach (PooledObject instance in _inactivePool)
            {
                System.Type enemyType = instance.GetComponent<Enemy>().GetType();
                if (allowedType == typeof(Enemy) || enemyType == allowedType)
                {
                    matchingEnemy = instance;
                    break;
                }
            }
            //Debug.Log($"allowed type is: {allowedType}, matching enemy is: {matchingEnemy}");
            return matchingEnemy;
        }

        public virtual void KillAll()
        {
            //SpawnCount = 0;
            List<PooledObject> enemiesToKill = new List<PooledObject>();
            enemiesToKill.AddRange(_activePool);
            foreach (PooledObject instance in enemiesToKill)
            {
                if (PrintDebugLogText) Debug.Log($"---- EnemyPool.KillAll(): killed {instance.name}, its enemy componenet was  {instance.GetComponent<Enemy>()}");
                instance.GetComponent<Enemy>().Dead();
            }
        }

    }
}