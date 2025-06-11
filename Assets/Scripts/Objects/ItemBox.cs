using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour,IGetItem
{
    [SerializeField] private ItemType item;
    [SerializeField] private AudioSource getSound;

    void Start()
    {
        if (getSound != null)
        {
            getSound.Stop();
        }
    }
    public virtual ItemType GetItem()
    {
        if (getSound != null && !getSound.isPlaying)
                getSound.Play();
        return item;
    } 

    public void SetType(ItemType type)
    {
        item = type;
    }
    public ItemType GetCurrentType()
    {
        return item;
    }
}
