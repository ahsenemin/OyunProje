
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ActionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int targetScore;
    [SerializeField] private int sceneIndex;
    private int currentScore = 0;

    private Animator anim;
    private Inventory inventory;
    private Functionality currentFunction;
    private WaitForSeconds takeCooldown;
    private bool isWorking = false;
    private bool isProcessing = false;
    private bool canPut = true;

    private void Awake() // Awake metodu, bileşenler henüz başlatılmadan önce çağrılır
    {
        canPut = true;
        anim = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        takeCooldown = new WaitForSeconds(0.5f);
    }

    private void Start() // Start metodu, bileşenler başlatıldıktan sonra çağrılır
    {
        UpdateScoreText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoAction();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            isWorking = true;
            if (isProcessing == false)
            {
                StartProcessAction();
            }
            else
            {
                DoProcessAction();
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            isWorking = false;
            if (isProcessing)
            {
                currentFunction?.ResetTimer();
                isProcessing = false;
            }
        }
    }

    private void DoAction() 
    {
        anim.SetTrigger("Take");
    }

    private void StartProcessAction() // StartProcessAction, oyuncunun etkileşimde bulunabileceği bir nesneye bakar
    {
        Ray ray = new Ray(transform.position + Vector3.up / 2, transform.forward); // Oyuncunun bakış yönünde bir ışın oluşturur
        if (Physics.Raycast(ray, out RaycastHit hit, 1)) // Işın, 1 birim mesafeye kadar çarpar
        {
            if (hit.collider.TryGetComponent<Functionality>(out Functionality itemProcess)) // Eğer çarpan nesne Functionality bileşenine sahipse
            {
                isProcessing = true;
                currentFunction = itemProcess;
            }
        }
    }

    private void DoProcessAction() // DoProcessAction, eğer isProcessing true ise ve currentFunction null değilse, işlemi gerçekleştirir
    {
        if (!isProcessing) return;
        if (!isWorking) return;
        ItemType item = currentFunction.Process();
        if (item != ItemType.NONE)
        {
            currentFunction.ClearObject();
            inventory.TakeItem(item);
            isWorking = false;
        }
    }

    public void DoTakeAction() // ALMA VERME İŞLERİ
    {
        Ray ray = new Ray(transform.position + Vector3.up / 2, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 1))
        {
            // Eliniz boşsa önce ItemBox'tan almayı deneyin
            if (inventory.CurrentType == ItemType.NONE)
            {
                if (hit.collider.TryGetComponent<ItemBox>(out ItemBox itemBox))
                {
                    ItemType item = itemBox.GetItem();
                    if (item != ItemType.NONE)
                    {
                        inventory.TakeItem(item);
                        StartCoroutine(canPutCoolDown());
                        return;
                    }
                }
            }

            // Öğeyi masaya veya başka bir kaba koymayı deneyin
            if (canPut && inventory.CurrentType != ItemType.NONE)
            {
                if (hit.collider.TryGetComponent<IPutItemFull>(out IPutItemFull container))
                {
                    ItemType itemToPut = inventory.GetItem();
                    if (container.PutItem(itemToPut))
                    {
                        inventory.ClearHand();
                        StartCoroutine(canPutCoolDown());
                    }
                }
            }
        }
    }
   // elinde hamburger varsa ve satış alanında ise, E tuşuna basınca hamburger satılır,
    private void OnTriggerStay(Collider other)
    {
        if (inventory.CurrentType != ItemType.HAMBURGER) return;
        if (other.gameObject.CompareTag("SellArea"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CustomerManager.Instance.SellToCustomer();
                inventory.ClearHand();
                IncreaseScore();
            }
        }
    }
    // sonra kısa bir süre beklenir, ardından tekrar koyma izni verilir.
    private IEnumerator canPutCoolDown()
    {
        canPut = false;
        yield return takeCooldown;
        canPut = true;
    }

    // Eğer bir hamburger satış alanına bırakılırsa skor artırılır.
    public void ItemDropped(ItemType itemType, Vector3 position)
    {
        if (IsInSellArea(position) && itemType == ItemType.HAMBURGER)
        {
            IncreaseScore();
        }
    }
     // Verilen pozisyonun satış alanı içinde olup olmadığını kontrol eder.
    private bool IsInSellArea(Vector3 position)
    {
        GameObject sellArea = GameObject.FindGameObjectWithTag("SellArea");
        if (sellArea != null)
        {
            Collider sellAreaCollider = sellArea.GetComponent<Collider>();
            if (sellAreaCollider != null)
            {
                return sellAreaCollider.bounds.Contains(position);
            }
        }
        return false;
    }

    // Skoru bir artırır, ekrana yazar ve hedefe ulaşıldıysa sahneyi değiştirir.
    private void IncreaseScore()
    {
        currentScore++;
        UpdateScoreText();
        Debug.Log("Skor arttı: " + currentScore);

        if (currentScore >= targetScore)
        {
            Debug.Log("Hedef skora ulaşıldı! Sahne değişiyor...");
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }

    // Skor bilgisini ekranda günceller.
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + currentScore + "/" + targetScore;
            Debug.Log("Skor text güncellendi: " + scoreText.text);
        }
        else
        {
            Debug.LogWarning("Score Text referansı eksik!");
        }
    }
}