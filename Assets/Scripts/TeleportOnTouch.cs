using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnTouch : MonoBehaviour
{
    public GameObject dogCharacter; // Inspector'dan Dog karakterini sürükleyerek atayacaksın
    public Vector3 spawnPoint = new Vector3(-75.327f, 29.762f, -1.908f);
    private Inventory playerInventory;

    private void Start()
    {
        playerInventory = dogCharacter.GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == dogCharacter)
        {
            dogCharacter.transform.position = spawnPoint;
            if (playerInventory != null)
            {
                playerInventory.ClearHand();
            }
        }
    }
}
