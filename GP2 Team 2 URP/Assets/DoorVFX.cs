using UnityEngine;

public class DoorVFX : MonoBehaviour
{
    [SerializeField] private GameObject _doorUnlockedVFX;
    [SerializeField] private GameObject _doorLockedVFX;
    public void StartDoorUnlockedVFX() {
        _doorUnlockedVFX.SetActive(true);
        _doorLockedVFX.SetActive(false);
    }

    public void StartDoorLockedVFX() {
        _doorUnlockedVFX.SetActive(false);
        _doorLockedVFX.SetActive(true);
        
    }
}
