

using UnityEngine;

[System.Serializable]

public class ItemTest
{

    public string name;
    public int count;

    public GameObject weaponPrefab;
    public GameObject instantiatedWeapon;

    public ItemTest(string itemName, int itemCount)
    {
        name = itemName;
        count = itemCount;
    }
}
