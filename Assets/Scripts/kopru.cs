using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kopru : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // Karakteri köprüye bağlıyoruz
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null); // Karakteri tekrar serbest bırakıyoruz
        }
    }
}
