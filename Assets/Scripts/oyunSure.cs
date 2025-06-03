using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class oyunSure : MonoBehaviour
{
    
    [Header("Süre Ayarları")]
    public float toplamSure = 300f; // 5 dakika (saniye cinsinden)
    public TextMeshProUGUI sureText; // UI'daki süre göstergesi
    [SerializeField] private int hedefSahneIndex; // Geçilecek sahnenin index'i

    private float kalanSure;
    private bool oyunDevamEdiyor = true;

    // Start is called before the first frame update
    void Start()
    {
        kalanSure = toplamSure;
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevamEdiyor)
        {
            kalanSure -= Time.deltaTime;
            SureGuncelle();

            if (kalanSure <= 0)
            {
                SahneyeGec();
            }
        }
    }

    void SureGuncelle()
    {
        int dakika = Mathf.FloorToInt(kalanSure / 60);
        int saniye = Mathf.FloorToInt(kalanSure % 60);
        
        if (sureText != null)
        {
            sureText.text = string.Format("{0:00}:{1:00}", dakika, saniye);
        }
    }

    void SahneyeGec()
    {
        oyunDevamEdiyor = false;
        SceneManager.LoadScene(hedefSahneIndex);
    }
}
