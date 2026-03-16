using UnityEngine;

public class PickUp : MonoBehaviour
{
    public ItemTest item = new ItemTest("Item Name ", 1);
    public string weaponName;
    public Sprite icon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Inventory.instance.currentWeapons >= Inventory.instance.maxWeapons)
            {
                Debug.Log("Cannot add more weapons, inventory is full!");
                return;
            }

            else
            {
                Inventory.instance.AddItem(item);
                Destroy(gameObject);
            }


        }
    }
}
