using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponUI : MonoBehaviour
{
    [SerializeField] PlayerController _pC;
    [SerializeField] PlayerAttack _playerAttack;
    [SerializeField] Image[] _handSlots;
    [SerializeField] Image[] _handSlotsHighlight;

    [SerializeField] Image[] _displayDurability;
    [SerializeField] Sprite _notMaxDurability;
    [SerializeField] Sprite _maxDurability;

    public Sprite defaultIcon;
    public Sprite icon;

    protected PlayingState _updateState;

    protected void OnEnable()
    {
        if (_updateState != null)
        {
            _updateState.StateUpdate.AddListener(ManagedUpdate);
        }
    }
    private void Start()
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
    }
    protected void OnDisable()
    {
        if (_updateState != null)
        {
            _updateState.StateUpdate.RemoveListener(ManagedUpdate);
        }
    }
    protected void ManagedUpdate()
    {
        UpdateHandUI();
        DisplayHealthShield();
    }

    void UpdateHandUI()
    {
        for (int i = 0; i < _handSlots.Length; i++)
        {
            if (_pC.handPositions[i].childCount > 0)
            {

                Weapon weapon = _pC.handPositions[i].GetChild(0).GetComponent<Weapon>();

                if (weapon != null)
                {

                    Image weaponImage = _handSlots[i].GetComponent<Image>();

                    if (weaponImage != null && weapon.icon != null)
                    {
                        weaponImage.sprite = weapon.icon;
                        weaponImage.enabled = true;
                    }
                }

                else
                {
                    ResetSlotToDefault(i);
                }
            }
            else
            {
                ResetSlotToDefault(i);
            }
        }

        //for(int i = 0; _playerAttack.comboStep <= 3; i++)
        //{
        //    _handSlotsHighlight[i].gameObject.SetActive(true);
        //}
        if(_playerAttack.comboStep == 0)
        {
            _handSlotsHighlight[1].gameObject.SetActive(false);
            _handSlotsHighlight[2].gameObject.SetActive(false);
            _handSlotsHighlight[3].gameObject.SetActive(false);
            _handSlotsHighlight[0].gameObject.SetActive(true);
        }
        if(_playerAttack.comboStep == 1)
        {
            _handSlotsHighlight[0].gameObject.SetActive(false);
            _handSlotsHighlight[2].gameObject.SetActive(false);
            _handSlotsHighlight[3].gameObject.SetActive(false);
            _handSlotsHighlight[1].gameObject.SetActive(true);
        }
        if(_playerAttack.comboStep == 2)
        {
            _handSlotsHighlight[1].gameObject.SetActive(false);
            _handSlotsHighlight[3].gameObject.SetActive(false);
            _handSlotsHighlight[0].gameObject.SetActive(false);
            _handSlotsHighlight[2].gameObject.SetActive(true);
        }
        if(_playerAttack.comboStep == 3)
        {
            _handSlotsHighlight[2].gameObject.SetActive(false);
            _handSlotsHighlight[0].gameObject.SetActive(false);
            _handSlotsHighlight[1].gameObject.SetActive(false);
            _handSlotsHighlight[3].gameObject.SetActive(true);
        }
    }

    void ResetSlotToDefault(int index)
    {

        Image weaponImage = _handSlots[index].GetComponent<Image>();
        if (weaponImage != null)
        {
            weaponImage.sprite = defaultIcon;
            weaponImage.enabled = true;
        }
    }

    void DisplayHealthShield()
    {
        for (int i = 0; i < _handSlots.Length; i++)
        {
            Image durabilityPosition = _displayDurability[i];

            if (_pC.handPositions[i].childCount > 0)
            {
                Weapon weapon = _pC.handPositions[i].GetChild(0).GetComponent<Weapon>();
                if (weapon == null)
                {
                    _displayDurability[i].gameObject.SetActive(false);
                    return;
                }
                if (weapon.durability != 0)
                {

                    if (weapon.durability < weapon.maxDurability)
                    {
                        _displayDurability[i].gameObject.SetActive(true);
                        durabilityPosition.sprite = _notMaxDurability;
                    }

                    if (weapon.durability == weapon.maxDurability)
                    {
                        _displayDurability[i].gameObject.SetActive(true);
                        durabilityPosition.sprite = _maxDurability;
                    }
                }

                else
                {
                    _displayDurability[i].gameObject.SetActive(false);
                    durabilityPosition.sprite = null;
                }
                    
            }
            else
            {
                _displayDurability[i].gameObject.SetActive(false);
                durabilityPosition.sprite = null;
            }
                

        }
    }
}
