using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : ItemBox, IPutItemFull
{
    [SerializeField] private List<ObjectnType> itemsToHold = new List<ObjectnType>();
    [SerializeField] private Plate plate;
    [SerializeField] private bool isFull;
    [SerializeField] private AudioSource putSound;

    private void Start()
    {
        SetType(ItemType.NONE);

         if (putSound != null)
        {
            putSound.Stop();
        }
    }

    public override ItemType GetItem()
    {
        if (GetCurrentType() == ItemType.PLATE && plate.isDone == false) { return ItemType.NONE; }
        else if (plate.isDone)
        {
            StartCoroutine(ChangeType());
            plate.ResetPlate();
             if (putSound != null && !putSound.isPlaying)
                putSound.Play();
            return ItemType.HAMBURGER;
        }
        else if (isFull)
        {
            StartCoroutine(ChangeType());
             if (putSound != null && !putSound.isPlaying)
                putSound.Play();
            return base.GetItem();
        }
        return ItemType.NONE;
    }

    public bool PutItem(ItemType item)
    {
        // ❌ Eğer yanmış etse, hiç alma
        if (item == ItemType.BURNEDMEAT) return false;

        if (GetCurrentType() == ItemType.PLATE)
        {
            if (item != ItemType.PLATE)
            {
                if (putSound != null && !putSound.isPlaying)
                putSound.Play();
                StartCoroutine(PutCoolDown());
                return plate.PutItem(item);
            }
        }

        if (!isFull && GetCurrentType() == ItemType.NONE)
        {
            SetType(item);
            if (putSound != null && !putSound.isPlaying)
                putSound.Play();
            foreach (ObjectnType itemHold in itemsToHold)
            {

                itemHold.item.SetActive(itemHold.type == GetCurrentType());
            }
            StartCoroutine(PutCoolDown());
            return true;
        }

        return false;
    }

    private IEnumerator PutCoolDown()
    {
        yield return new WaitForEndOfFrame();
        isFull = true;
    }

    private IEnumerator ChangeType()
    {
        CloseItem();
        yield return new WaitForEndOfFrame();
        SetType(ItemType.NONE);
        isFull = false;
    }

    public void CloseItem()
    {
        itemsToHold.ForEach(item => item.item.SetActive(false));
    }
}