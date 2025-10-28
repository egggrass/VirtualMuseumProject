using GLTFast.Schema;
using UnityEngine;

public class PickUp : MonoBehaviour, Interactable
{
    public GameObject bigAxe;
    public GameObject smallAxe;
    private bool isActive = false;
     public bool playerInRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        isActive = !isActive;
        bigAxe.SetActive(isActive);
        smallAxe.SetActive(!isActive);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            isActive = false;
            bigAxe.SetActive(false);
            smallAxe.SetActive(true);
        }
    }
}
