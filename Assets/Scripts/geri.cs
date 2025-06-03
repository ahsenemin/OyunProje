using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class geri : MonoBehaviour
{
    
    [SerializeField] private Button geriButonu;
    [SerializeField] private int sceneIndex; // Geri dönülecek sahnenin index'i

    // Update is called once per frame
    void Awake()
    {
        geriButonu.onClick.AddListener(() =>{
            SceneManager.LoadScene(sceneIndex);
        });
    }
}
