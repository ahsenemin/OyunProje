using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemType itemType;
    private ActionController actionController;

    private void Start()
    {
        actionController = FindObjectOfType<ActionController>();
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    private void OnDestroy()
    {
        if (actionController != null)
        {
            actionController.ItemDropped(itemType, transform.position);
        }
    }
}
