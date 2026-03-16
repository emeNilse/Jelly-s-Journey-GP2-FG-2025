using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public PlayerAttack PA;

    [Header("Debug Stuff")]
    public bool PrintDebugLogs = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                int damage = PA.CalculateDamage();
                if (PrintDebugLogs) Debug.Log($"I ({name}) HIT AN ENEMY ({enemy.name})");
                enemy.TakeDamage(damage);
                if (PrintDebugLogs) Debug.Log($"LOOK MOM, I ({name}) DID THIS MUCH DAMAGE: {damage}");
            }
            //if (other.TryGetComponent<MeleeEnemy>(out MeleeEnemy enemy))
            //{
            //    enemy.TakeDamage(PA.CalculateDamage());
            //    if(PrintDebugLogs) Debug.Log("LOOK MOM, I DID THIS MUCH DAMAGE: " + PA.CalculateDamage());
            //}

            //else if (other.TryGetComponent<RangedEnemy>(out RangedEnemy rangedenemy))
            //{
            //    rangedenemy.TakeDamage(PA.CalculateDamage());
            //    if (PrintDebugLogs) Debug.Log("LOOK MOM, I DID THIS MUCH DAMAGE: " + PA.CalculateDamage());
            //}
            //else { if (PrintDebugLogs) Debug.Log("LOOK MOM, I DID THIS MUCH DAMAGE: " + PA.CalculateDamage()); }
        }
    }

}
