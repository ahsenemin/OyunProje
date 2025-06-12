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
    public virtual ItemType GetItem() //eldeki ürünü kontrol eder. 
    {
        if (getSound != null && !getSound.isPlaying)
                getSound.Play();
        return item;
    } 

    public void SetType(ItemType type) // Öğe tipini ayarla.unityden ayarlamasını sağlar. nesnenin türünü ayarlar
    // Bu metot, ItemType enum'undan bir değer alır ve item değişkenine atar.
    {
        item = type;
    }
    public ItemType GetCurrentType() // elimizde hangi öge olduğunu döndürür. oyun sırasında nesnenin ne olduğunu kontrol eder.
    {
        return item;
    }
}
