using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{

    public List<Weapon> lootList = new List<Weapon>();

    [SerializeField] private float _dropForce;

    Weapon GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<Weapon> possibleItems = new List<Weapon>();
        foreach(Weapon weapon in lootList)
        {
            if(randomNumber <= weapon.dropChance)
            {
                possibleItems.Add(weapon);
            }
        }

        if(possibleItems.Count > 0)
        {
            Weapon droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("NO ITEM DROPPED");
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Weapon droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItem.gameObject, spawnPosition, Quaternion.identity);
            //lootGameObject.GetComponent<SpriteRenderer>.sprite = droppedItem.lootSprite;

            //Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            //lootGameObject.GetComponent<Rigidbody>().AddForce(dropDirection * _dropForce, ForceMode.Impulse);
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    //[System.Serializable]
    //public class Drops
    //{
    //    public string name;
    //    public GameObject itemPrefab;
    //    public float dropRate;
    //}

    //public List<Drops> drops;

    //public void OnDespawn()
    //{
    //    float randomNumber = Random.Range(0f, 100f);
    //    List<Drops> possibleDrops = new List<Drops>();

    //    foreach (Drops drop in drops)
    //    {
    //        if (randomNumber <= drop.dropRate)
    //        {
    //            possibleDrops.Add(drop);
    //        }

    //        //checks if there are possible drops
    //        if (possibleDrops.Count > 0)
    //        {
    //            Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
    //            Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
    //        }
    //    }
    //}
}
