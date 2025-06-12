using System.Collections;
using UnityEngine;

public class ElectricManager : MonoBehaviour
{
    public Oven ovenScript;                 // Oven scripti doğrudan referans alınıyor
    public Light directionalLight;          // Sahnedeki Directional Light
    public int totalPowerCuts = 3;          // Toplam kaç kez elektrik gidecek
    public float initialDelay = 5f;         // Oyun başladıktan sonra ilk elektrik kesintisine kadar bekleme
    public float minTimeBetweenCuts = 5f;   // Elektrik kesintileri arasındaki minimum süre
    public float maxTimeBetweenCuts = 10f;  // Maksimum süre

    

    private int currentCuts = 0;
    private bool powerOn = true;
    private bool isCuttingPower = false;

    private void Start()
    {
        StartCoroutine(HandlePowerCuts());
    }

    private IEnumerator HandlePowerCuts()
    {
        yield return new WaitForSeconds(initialDelay);

        while (currentCuts < totalPowerCuts)
        {
            if (powerOn && !isCuttingPower)
            {
                isCuttingPower = true;

                float waitTime = Random.Range(minTimeBetweenCuts, maxTimeBetweenCuts);
                yield return new WaitForSeconds(waitTime);

                CutPower();
                currentCuts++;
            }

            yield return null;
        }
    }

    private void CutPower()
    {
        if (!powerOn) return;

        powerOn = false;
        ovenScript.enabled = false;
        directionalLight.enabled = false;
        Debug.Log("⚡ Elektrik kesildi!");
    }

    public void RestorePower()
    {
        if (!powerOn)
        {
            powerOn = true;
            ovenScript.enabled = true;
            directionalLight.enabled = true;
            isCuttingPower = false;
            Debug.Log("✅ Elektrik geri geldi!");
        }
    }

    public bool IsPowerOn()
    {
        return powerOn;
    }
}
