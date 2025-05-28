using UnityEngine;

public class ElectricSwitch : MonoBehaviour
{
    public ElectricManager electricManager;
    public GameObject dogPlayer;  // Oyuncu objesi doğrudan referans olarak atanacak
    private bool isPlayerNearby = false;

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            electricManager.RestorePower();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == dogPlayer)
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == dogPlayer)
        {
            isPlayerNearby = false;
        }
    }
}
