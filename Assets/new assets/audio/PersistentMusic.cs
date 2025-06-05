using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Aynı müzik varsa yeni olanı yok et
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Bu nesne sahneler arasında silinmesin
    }
}
