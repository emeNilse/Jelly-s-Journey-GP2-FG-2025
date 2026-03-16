using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeaponUITest : MonoBehaviour
{
    [SerializeField] PlayerController _pC;
    [SerializeField] Image[] _handSlots;
    [SerializeField] TextMeshProUGUI[] _handWeaponText;

    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    public Sprite icon;
    public Sprite defaultIcon;

    void Update()
    {
        UpdateHandUI();
    }

    void UpdateHandUI()
    {
        for (int i = 0; i < _handSlots.Length; i++)
        {

            if (Inventory.instance.characterHands[i].transform.childCount > 0)
            {
                PickUp weapon = Inventory.instance.characterHands[i].transform.GetChild(0).GetComponent<PickUp>();
                if (weapon != null)
                {
                    _handWeaponText[i].text = weapon.weaponName;

                    Image weaponImage = _handSlots[i].GetComponent<Image>();
                    
                    if(weaponImage != null && weapon.icon != null)
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

    }
    void ResetSlotToDefault(int index)
    {
        _handWeaponText[index].text = "Empty";

        Image weaponImage = _handSlots[index].GetComponent<Image>();
        if (weaponImage != null)
        {
            weaponImage.sprite = defaultIcon;
            weaponImage.enabled = true;
        }
    }
}
