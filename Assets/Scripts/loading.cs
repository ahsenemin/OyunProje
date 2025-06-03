using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loading : MonoBehaviour
{
    [SerializeField] private float beklemeSuresi = 3f; // 3 saniye bekleme süresi
    [SerializeField] private int nextScene = 1;
    void Start()
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
        // Eğer sahne skybox kullanıyorsa bu daha uygun olur
        RenderSettings.ambientLight = Color.white;

        StartCoroutine(SahneyeGec());
    }

    IEnumerator SahneyeGec()
    {
        yield return new WaitForSeconds(beklemeSuresi);
        SceneManager.LoadScene(nextScene); // Her zaman 1. sahneye geçiş yapar
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
