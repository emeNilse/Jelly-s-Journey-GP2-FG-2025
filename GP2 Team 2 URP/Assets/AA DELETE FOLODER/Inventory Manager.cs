using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public List<GameObject> equipedItems;

    //Stack<InventorySlot> inventorySlotsStack;

    private int _selectedSlot = -1;


    private void Start()
    {
        ChangeSelectedSlot(0);
        
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 5)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    private void ChangeSelectedSlot(int newValue)
    {
        if (_selectedSlot >= 0)
            inventorySlots[_selectedSlot].DeSelect();

        inventorySlots[newValue].Select();
        _selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void RemoveItem()
    {
        for (int i = inventorySlots.Length -1; i >= 0; i--)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                //Change parent TO WHAT YOU NEED (FIGURE IT OUT)
                //DO NOT REMOVE THIS ITEM
                Destroy(itemInSlot.gameObject);
                equipedItems.RemoveAt(equipedItems.Count -1); //////////////////////////////////////////////////////////
                print("Removed Item");
                return;
            }
        }
        Debug.Log("I AM PAST THIS SHIT");
    }

    public InventoryItem SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        equipedItems.Add(newItemGo);/////////////////////////////////////////////////////////////////////////
        return inventoryItem;
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[_selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            return itemInSlot.item;
        }

        return null;
    }
}
