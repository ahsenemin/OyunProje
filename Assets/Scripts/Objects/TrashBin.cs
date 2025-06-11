using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour,IPutItemFull
{
        [SerializeField] private AudioSource putSound;
    void Start()
    {
        if (putSound != null)
        {
            putSound.Stop();
        }
    }
         
    public bool PutItem(ItemType item) // Bu yöntem, bir öğe çöp kutusuna atıldığında çağrılır.
    {
        if (putSound != null && !putSound.isPlaying)
            putSound.Play();
        return true;
    }
     
}
