using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    [SerializeField] private float timerSpeed;
    [SerializeField] private Queue<Customer> customerList = new Queue<Customer>();

    [SerializeField] private List<Customer> customerPrefabs = new List<Customer>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform exitPoint;
    private float currentTime = 0;
    private float lrRandom=0.75f;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (currentTime <=Random.Range(50,80))// müşteri random oluşturma süresi
        {
            currentTime += Time.deltaTime*timerSpeed; // zaman ilerlemesi
        }
        else
        {
            currentTime = 0;
            Vector3 spawnPos = spawnPoint.position + (spawnPoint.forward * -1 * customerList.Count)+spawnPoint.right*Random.Range(-lrRandom, lrRandom);
            // Müşterinin spawn pozisyonu, spawn noktasının gerisinde ve sağa/sola rastgele kaydırılmış bir şekilde pozisyon ayarlıyor
            Customer temp = Instantiate(customerPrefabs[Random.Range(0,customerPrefabs.Count)], spawnPos, spawnPoint.rotation);
            // Rastgele müşteri prefab'ı oluşturma (rastgele bir müşteri türü seçiliyor)
            customerList.Enqueue(temp); // Müşteriyi kuyruğa ekleme 
        } 
    }
    public void SellToCustomer() // ilk müşteriyi exitpoint'e yönlendirir ve kuyruğun başındaki müşteriyi çıkarır
    {
        if (customerList.Count == 0) return; 
        Customer firstCustomer=customerList.Peek(); 
        firstCustomer.ExitFromArea(exitPoint.position); 
        customerList.Dequeue();
        for(int i = 0; i < customerList.Count; i++)
        {
            Vector3 nextPos = spawnPoint.position + (spawnPoint.forward * -1 * i) + spawnPoint.right * Random.Range(-lrRandom, lrRandom); // Müşterilerin yeni pozisyonları
            customerList.ToArray()[i].MoveNext(nextPos); // Müşterileri yeni pozisyonlarına yönlendirme
        } 

    }
}
