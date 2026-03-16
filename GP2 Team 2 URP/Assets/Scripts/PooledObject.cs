using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    [SerializeField] float timeBeforeRecycle;
    protected float recycleTimer = 0;

    public UnityEvent<PooledObject> OnReturnToPool;

    private void OnEnable()
    {
        recycleTimer = 0;
    }
    protected void OnDisable()
    {
    }

    public virtual void ReturnToPool()
    {
        //trying out resetting values in onEnable right now
        if(OnReturnToPool != null)
        {
            OnReturnToPool.Invoke(this);

        }
    }

    public virtual void PoolUpdate()
    {
    }
    
    public virtual void PoolFixedUpdate() { }
    protected virtual float CheckTimer(float timer, float threshhold)
    {
        if (timer >= threshhold)
        {
            timer = 0f;
            return timer;
        }
        else
        {
            timer += Time.deltaTime;
            return timer;
        }
    }
}
