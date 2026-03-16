using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemeySoundEffects : MonoBehaviour
{
    [SerializeField, Tooltip("GUID or Path")] private string enemyMeleeAttackSoundEffect = "event:/sfx/characters/enemy/Attack";
    [SerializeField, Tooltip("GUID or Path")] private string enemyRangedAttackSoundEffect = "event:/sfx/characters/enemy/RangedAttack";
    [SerializeField, Tooltip("GUID or Path")] private string enemyDeathSoundEffect = "event:/sfx/characters/enemy/Die";
    [SerializeField, Tooltip("GUID or Path")] private string enemyGetHitSoundEffect = "event:/sfx/characters/enemy/GetHit"; 

    [SerializeField] Transform enemyTransform;
    [SerializeField] Rigidbody enemyRigidbody;

    private EventInstance enemyMeleeAttackSFX;
    private EventInstance enemyRangedAttackSFX;
    private EventInstance enemyDeathSFX;
    private EventInstance enemyGetHitSFX;

    void Start() {
        if (enemyTransform == null) {
            enemyTransform = transform;
        }

        if (enemyRigidbody == null) {
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        // Create the sound effects
        if (enemyMeleeAttackSoundEffect != null && enemyMeleeAttackSoundEffect != "") {
            enemyMeleeAttackSFX = RuntimeManager.CreateInstance(enemyMeleeAttackSoundEffect);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemyMeleeAttackSFX, enemyTransform, enemyRigidbody);
        }

        if (enemyRangedAttackSoundEffect != null && enemyRangedAttackSoundEffect != "") {
            enemyRangedAttackSFX = RuntimeManager.CreateInstance(enemyRangedAttackSoundEffect);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemyRangedAttackSFX, enemyTransform, enemyRigidbody);
        }

        if (enemyDeathSoundEffect != null && enemyDeathSoundEffect != "") {
            enemyDeathSFX = RuntimeManager.CreateInstance(enemyDeathSoundEffect);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemyDeathSFX, enemyTransform, enemyRigidbody);
        }

        if (enemyGetHitSoundEffect != null && enemyGetHitSoundEffect != "") {
            enemyGetHitSFX = RuntimeManager.CreateInstance(enemyGetHitSoundEffect);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(enemyGetHitSFX, enemyTransform, enemyRigidbody);
        }

        // Set parameters for the sound effects as well if needed
    }

    void PlayEnemyMeleeAttackSFX() {

        // Check to make sure the sound effect is not null
        if (!enemyMeleeAttackSFX.isValid()) {
            return;
        }

        // Play the enemy attack sound effect
        enemyMeleeAttackSFX.start();
    }

    void PlayEnemyRangedAttackSFX() {
        // Check to make sure the sound effect is not null
        if (!enemyRangedAttackSFX.isValid()) {
            return;
        }
        // Play the enemy ranged attack sound effect
        enemyRangedAttackSFX.start();
    }

    void PlayEnemyDeathSFX() {
        // Check to make sure the sound effect is not null
        if (!enemyDeathSFX.isValid()) {
            return;
        }
        // Play the enemy death sound effect
        enemyDeathSFX.start();
    }

    void PlayEnemyGetHitSFX() {
        // Check to make sure the sound effect is not null
        if (!enemyGetHitSFX.isValid()) {
            return;
        }
        // Play the enemy get hit sound effect
        enemyGetHitSFX.start();
    }

    void OnDestroy() {
        // Release the sound effects
        enemyMeleeAttackSFX.release();
        enemyRangedAttackSFX.release();
        enemyDeathSFX.release();
    }

}
