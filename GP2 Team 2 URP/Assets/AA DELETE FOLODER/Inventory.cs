using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject[] characterHands;
    public int currentWeapons;
    public int maxWeapons = 4;
    public List<ItemTest> itemsEquiped = new List<ItemTest>();



    Vector3 throwdirection;




    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;


        throwdirection = transform.forward + Vector3.up;
    }



    public void AddItem(ItemTest itemToAdd)
    {
        if (currentWeapons >= maxWeapons)
        {
            Debug.Log("Cannot add more weapons, inventory is full!");
            return;
        }

        for (int i = 0; i < characterHands.Length; i++)
        {
            if (characterHands[i].transform.childCount == 0)
            {
                GameObject weapon = Instantiate(itemToAdd.weaponPrefab, characterHands[i].transform);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
                weapon.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                itemToAdd.instantiatedWeapon = weapon;
                currentWeapons++;

                itemsEquiped.Add(itemToAdd);
                Debug.Log($"Weapon added to hand {i}");
                return;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (itemsEquiped.Count > 0)
            RemoveItem(itemsEquiped.Last());
            else 
                return;
        }
            
    }

    public void RemoveItem(ItemTest itemToRemove)
    {

        for (int i = 0; i < characterHands.Length; i++)
        {
            if (characterHands[i].transform.childCount == 1) 
            {
                itemToRemove.instantiatedWeapon.transform.parent = null;
                itemToRemove.instantiatedWeapon.GetComponent<Rigidbody>().isKinematic = false;

                itemToRemove.instantiatedWeapon.GetComponent<Rigidbody>().AddForce(throwdirection.normalized * 5f, ForceMode.Impulse);

                currentWeapons--;
                itemsEquiped.Remove(itemToRemove);
                Debug.Log($"Weapon removed to hand {i}");
                return;
            }

            if (characterHands[i].transform.childCount == 0 )
            {
                currentWeapons--;
                itemsEquiped.Remove(itemToRemove);
                Debug.Log($"Weapon removed to hand {i}");
                return;
            }
            if (itemToRemove.count == 0)
            {
                return;
            }
        }
    }
}

