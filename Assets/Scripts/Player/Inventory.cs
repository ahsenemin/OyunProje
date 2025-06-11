using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectnType
{
    public GameObject item;
    public ItemType type;
}

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ObjectnType> itemsToHold = new List<ObjectnType>();
    private ItemType currentType;
    public ItemType CurrentType { get { return currentType; } } // Property to access currentType

    private void Start()
    {
        currentType = ItemType.NONE;
    }

    public void TakeItem(ItemType type)
    {
        if (currentType != ItemType.NONE) return; // EĞER HALDE ELDE BİR ÖGE VARSA YENİSİNİ ALMA
        currentType = type;
        foreach (ObjectnType itemHold in itemsToHold) // Tüm ögeleri kontrol et
        {
            itemHold.item.SetActive(itemHold.type == type);  // Eğer öge türü eşleşiyorsa, o ögeyi aktif et
        }
    }

    public ItemType PutItem() // Ögeyi elden bırakma işlemi
    {
        if (currentType == ItemType.NONE) return ItemType.NONE;
        ItemType itemToPut = currentType;
        ClearHand();
        return itemToPut;
    }

    public void ClearHand()
    {
        currentType = ItemType.NONE;
        itemsToHold.ForEach(obj => obj.item.SetActive(false));
    }

    public ItemType GetItem()
    {
        return currentType;
    }
}