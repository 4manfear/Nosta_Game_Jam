using UnityEngine;

//=================================================================================ENUMS=================================================================================
public enum ItemCategory{ Collectible, NonCollectible }


[CreateAssetMenu(fileName = "NewItemType", menuName = "Item Type")]
public class ItemType : ScriptableObject
{
    public ItemCategory category; // Enum to define the type of item
    public GameObject[] itemPrefabs; // Array of prefabs for this item type
}

