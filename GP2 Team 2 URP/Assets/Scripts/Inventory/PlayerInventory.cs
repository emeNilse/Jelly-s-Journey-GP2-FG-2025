using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    int _maxCap = 4;

    public bool AddWeapon(GameObject weapon)
    {
        if (weapons.Count < _maxCap)
        {
            weapons.Add(weapon);
            return true;
        }
        else { return false; }
    }

    public void RemoveItem(GameObject weapon)
    {
        if (weapons.Contains(weapon))
        {
            weapons.Remove(weapon);
        }
    }    
    public void ThrowItem(GameObject weapon)
    {
        if (weapons.Contains(weapon))
        {
            weapons.RemoveAt(0);
        }
    }

    public void Initialize()
    {
            weapons.Clear();
    }
}
