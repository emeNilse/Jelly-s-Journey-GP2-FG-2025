using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class PlayerSoundEffects : MonoBehaviour
{
    [SerializeField, Tooltip("GUID or Path")] private string attack1SoundEffect = "event:/sfx/characters/player/Attack1";
    [SerializeField, Tooltip("GUID or Path")] private string attack2SoundEffect = "event:/sfx/characters/player/Attack2";
    [SerializeField, Tooltip("GUID or Path")] private string attack3SoundEffect = "event:/sfx/characters/player/Attack3";
    [SerializeField, Tooltip("GUID or Path")] private string attack4SoundEffect = "event:/sfx/characters/player/Attack4";
    [SerializeField, Tooltip("GUID or Path")] private string unarmedAttackSoundEffect = "event:/sfx/characters/player/UnarmedAttack";
    [SerializeField, Tooltip("GUID or Path")] private string throwSoundEffect = "event:/sfx/characters/player/ThrowItem";
    [SerializeField, Tooltip("GUID or Path")] private string deathSoundEffect = "event:/sfx/characters/player/Die";
    [SerializeField, Tooltip("GUID or Path")] private string getHitSoundEffect = "event:/sfx/characters/player/GetHit";
    [SerializeField, Tooltip("GUID or Path")] private string footstepSoundEffect = "event:/sfx/characters/player/Footsteps";
    [SerializeField, Tooltip("GUID or Path")] private string dashSoundEffect = "event:/sfx/characters/player/Dash";
    [SerializeField, Tooltip("GUID or Path")] private string spawnSoundEffect = "event:/sfx/characters/player/Spawn";


    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody playerRigidbody;

    private EventInstance playerAttack1SFX;
    private EventInstance playerAttack2SFX;
    private EventInstance playerAttack3SFX;
    private EventInstance playerAttack4SFX;
    private EventInstance playerUnarmedAttackSFX;

    private EventInstance playerThrowSFX;
    private EventInstance playerDeathSFX;
    private EventInstance playerGetHitSFX;
    private EventInstance playerFoostepSFX;
    private EventInstance playerDashSFX;
    private EventInstance playerSpawnSFX;

    void Start() {
        if (playerTransform == null) {
            playerTransform = transform;
        }

        if (playerRigidbody == null) {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        // Create the sound effects
        if (unarmedAttackSoundEffect != null && unarmedAttackSoundEffect != "") {
            playerUnarmedAttackSFX = RuntimeManager.CreateInstance(unarmedAttackSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerUnarmedAttackSFX, playerTransform, playerRigidbody);
        }

        if (attack1SoundEffect != null && attack1SoundEffect != "") {
            playerAttack1SFX = RuntimeManager.CreateInstance(attack1SoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerAttack1SFX, playerTransform, playerRigidbody);
        }

        if (attack2SoundEffect != null && attack2SoundEffect != "") {
            playerAttack2SFX = RuntimeManager.CreateInstance(attack2SoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerAttack2SFX, playerTransform, playerRigidbody);
        }

        if (attack3SoundEffect != null && attack3SoundEffect != "") {
            playerAttack3SFX = RuntimeManager.CreateInstance(attack3SoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerAttack3SFX, playerTransform, playerRigidbody);
        }

        if (attack4SoundEffect != null && attack4SoundEffect != "") {
            playerAttack4SFX = RuntimeManager.CreateInstance(attack4SoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerAttack4SFX, playerTransform, playerRigidbody);
        }

        if (throwSoundEffect != null && throwSoundEffect != "") {
            playerThrowSFX = RuntimeManager.CreateInstance(throwSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerThrowSFX, playerTransform, playerRigidbody);
        }

        if (deathSoundEffect != null && deathSoundEffect != "") {
            playerDeathSFX = RuntimeManager.CreateInstance(deathSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerDeathSFX, playerTransform, playerRigidbody);
        }

        if (getHitSoundEffect != null && getHitSoundEffect != "") {
            playerGetHitSFX = RuntimeManager.CreateInstance(getHitSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerGetHitSFX, playerTransform, playerRigidbody);
        }

        if (footstepSoundEffect != null && footstepSoundEffect != "") {
            playerFoostepSFX = RuntimeManager.CreateInstance(footstepSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerFoostepSFX, playerTransform, playerRigidbody);
        }

        if (dashSoundEffect != null && dashSoundEffect != "") {
            playerDashSFX = RuntimeManager.CreateInstance(dashSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerDashSFX, playerTransform, playerRigidbody);
        }

        if (spawnSoundEffect != null && spawnSoundEffect != "") {
            playerSpawnSFX = RuntimeManager.CreateInstance(spawnSoundEffect);
            RuntimeManager.AttachInstanceToGameObject(playerSpawnSFX, playerTransform, playerRigidbody);
        }

        // Set parameters for the sound effects as well if needed
    }

    void PlayPlayerAttack1SFX() {
        // Check to make sure the sound effect is not null
        if (!playerAttack1SFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerAttack1SFX.start();

    }

    void PlayPlayerAttack2SFX() {
        // Check to make sure the sound effect is not null
        if (!playerAttack2SFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerAttack2SFX.start();

    }

    void PlayPlayerAttack3SFX() {
        // Check to make sure the sound effect is not null
        if (!playerAttack3SFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerAttack3SFX.start();

    }

    void PlayPlayerAttack4SFX() {
        // Check to make sure the sound effect is not null
        if (!playerAttack4SFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerAttack4SFX.start();

    }

    void PlayPlayerUnarmedAttackSFX() {
        // Check to make sure the sound effect is not null
        if (!playerUnarmedAttackSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerUnarmedAttackSFX.start();

    }

    void PlayPlayerThrowSFX() {
        // Check to make sure the sound effect is not null
        if (!playerThrowSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerThrowSFX.start();

    }

    void PlayPlayerDeathSFX() {
        // Check to make sure the sound effect is not null
        if (!playerDeathSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerDeathSFX.start();

    }

    void PlayPlayerGetHitSFX() {
        // Check to make sure the sound effect is not null
        if (!playerGetHitSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerGetHitSFX.start();
    }

    void PlayPlayerFootstepSFX() {
        // Check to make sure the sound effect is not null
        if (!playerFoostepSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerFoostepSFX.start();
    }

    void PlayPlayerDashSFX() {
        // Check to make sure the sound effect is not null
        if (!playerDashSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerDashSFX.start();
    }

    void PlayPlayerSpawnSFX() {
        // Check to make sure the sound effect is not null
        if (!playerSpawnSFX.isValid()) {
            return;
        }
        // Play the player attack sound effect
        playerSpawnSFX.start();
    }

    void OnDestroy() {
        playerAttack1SFX.release();
        playerAttack2SFX.release();
        playerAttack3SFX.release();
        playerAttack4SFX.release();
        playerUnarmedAttackSFX.release();
        playerThrowSFX.release();
        playerDeathSFX.release();
        playerGetHitSFX.release();
        playerFoostepSFX.release();
        playerDashSFX.release();
        playerSpawnSFX.release();
    }
}