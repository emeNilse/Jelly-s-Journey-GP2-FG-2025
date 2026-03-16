using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    protected PlayingState _updateState;

    private Rigidbody _rb;
    CapsuleCollider _cc;

    public string weaponName;
    public Sprite icon;
    public bool isPickedUp = false;
    public bool isThrown = false;
    public int damage;
    public int dropChance;
    public int durability;
    public int maxDurability;
    public int thrownDamage;

    bool doneOnce = false;

    float pickedupTimer = 2f;


    void Start()
    {
        _updateState = GameManager.Instance.GetState<PlayingState>();
        if (_updateState != null)
        {
            _updateState.StateUpdate.AddListener(ManagedUpdate);
        }
        else
        {
            Debug.Log("tried to add listener but myUpdateState == null");
        }

        _rb = GetComponent<Rigidbody>();
        _cc = GetComponent<CapsuleCollider>();
    }

    void ManagedUpdate()
    {
        if (isThrown && !doneOnce)
        {
            ThrowSlowDown();
            isPickedUp = false;
            doneOnce = true;

        }

        pickedupTimer -= Time.deltaTime;
        if (pickedupTimer < 0) { isPickedUp = false; pickedupTimer = 2f; }
    }

    void ThrowSlowDown()
    {
        StartCoroutine(SlowDownWeapon());
    }

    private IEnumerator SlowDownWeapon()
    {
        float slowDownDuration = 1f;
        float elapsedTime = 0f;
        _rb.useGravity = true;
        _cc.enabled = true;
        isPickedUp = false;

        while (elapsedTime < slowDownDuration)
        {
            _rb.linearVelocity *= 0.99999f;
            elapsedTime += Time.deltaTime;
            //isPickedUp = false;
            yield return null;
        }

        isThrown = false;
        doneOnce = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && isThrown)
        {
            if (other.TryGetComponent<MeleeEnemy>(out MeleeEnemy enemy))
            {
                enemy.TakeDamage(thrownDamage);
                enemy.Stunned();
                isThrown = false;
                Debug.Log("LOOK MOM, I DID THIS MUCH DAMAGE: " + thrownDamage);
            }

            else if (other.TryGetComponent<RangedEnemy>(out RangedEnemy rangedenemy))
            {
                rangedenemy.TakeDamage(thrownDamage);
                rangedenemy.Stunned();
                isThrown = false;
                Debug.Log("LOOK MOM, I DID THIS MUCH DAMAGE: " + thrownDamage);
            }
            else { Debug.Log("LOOK MOM, I DID THIS MUCH DAMAGE: " + thrownDamage); }
        }
    }
}
