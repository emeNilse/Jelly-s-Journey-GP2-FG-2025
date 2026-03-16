using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]


public class Item : ScriptableObject
{
    [Header("Only gameplayer")]
    public ItemType type;
    //public ActionType actionType;
    public Vector3Int range = new Vector3Int(5, 5, 5);

    [Header("Only UI")]


    [Header("Both")]
    public Sprite image;

    public enum ItemType
    {
        Sword,
        Club,
        Axe,
        Stick
    }

    
}




//public enum ActionType
//{
//    Attack
//}

