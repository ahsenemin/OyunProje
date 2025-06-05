using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour, IPutItemFull
{
    private bool isFull;
    private bool isCooked;
    [SerializeField] private GameObject cookedItem;
    [SerializeField] private GameObject rawMeatball;
    [SerializeField] private GameObject burnedItem;
    [SerializeField] private UITimer timer;
    [SerializeField] private OvenBox itemBox;
    [SerializeField] private float minCookTime = 3f; // Minimum pişme süresi
    [SerializeField] private float maxCookTime = 7f; // Maximum pişme süresi
    [SerializeField] private float burnTime;
    [SerializeField] private GameObject particleEffect;
    [SerializeField] private AudioSource cookingSound; // Pişirme sesi için AudioSource
    private float currentTime;
    private float burnTimer;
    private float cookTime; // Random pişme süresi

    private void Start()
    {
        timer.gameObject.SetActive(false);
        cookedItem.SetActive(false);
        rawMeatball.SetActive(false);
        burnedItem.SetActive(false);
        
        // AudioSource'u başlangıçta durdur
        if (cookingSound != null)
        {
            cookingSound.Stop();
        }
    }

    private void Update()
    {
        if (isFull && !isCooked)
        {
            currentTime += Time.deltaTime;

            // Pişme sırasında timer'ı açık tut ve yeşil göster
            if (!timer.gameObject.activeSelf)
                timer.gameObject.SetActive(true);

            // Pişme sesini başlat
            if (cookingSound != null && !cookingSound.isPlaying)
                cookingSound.Play();

            timer.UpdateClock(currentTime, cookTime, false); // 🟢 pişirme
            if (currentTime >= cookTime)
            {
                currentTime = 0;
                timer.gameObject.SetActive(false);
                cookedItem.SetActive(true);
                rawMeatball.SetActive(false);
                itemBox.SetType(ItemType.COOKEDMEAT);
                itemBox.canTake = true;
                isFull = false;
                isCooked = true;
                burnTimer = 0f;
                particleEffect.SetActive(true);

                // Pişme bittiğinde sesi durdur
                if (cookingSound != null)
                    cookingSound.Stop();
            }
        }
        else if (isCooked)
        {
            burnTimer += Time.deltaTime;
            
            // Yanma sırasında da ses çalmaya devam et
            if (cookingSound != null && !cookingSound.isPlaying)
                cookingSound.Play();

            // Yanma sırasında timer'ı açık tut ve kırmızı göster
            if (!timer.gameObject.activeSelf)
                timer.gameObject.SetActive(true);

            timer.UpdateClock(burnTimer, burnTime, true); // 🔴 yanma

            if (burnTimer >= burnTime)
            {
                cookedItem.SetActive(false);
                burnedItem.SetActive(true);
                itemBox.SetType(ItemType.BURNEDMEAT);
                isCooked = false;
                timer.gameObject.SetActive(false);
                particleEffect.SetActive(true);
            }
        }
    }

    public void CloseCookedMeatObject()
    {
        cookedItem.SetActive(false);
        burnedItem.SetActive(false);
        isCooked = false;
        timer.gameObject.SetActive(false);
        particleEffect.SetActive(false);
        
        // Item alındığında sesi durdur
        if (cookingSound != null)
            cookingSound.Stop();
    }

    public bool PutItem(ItemType item)
    {
        if (item != ItemType.MEATBALL) return false;
        if (isFull) return false;
        
        // Random pişme süresi belirle
        cookTime = Random.Range(minCookTime, maxCookTime);
        
        timer.gameObject.SetActive(true);
        rawMeatball.SetActive(true);
        isFull = true;
        isCooked = false;
        particleEffect.SetActive(true);
        return true;
        
    }
}
