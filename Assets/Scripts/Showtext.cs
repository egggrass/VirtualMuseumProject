using UnityEngine;

public class Showtext : MonoBehaviour
{
    public GameObject text;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (text != null)
                text.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (text != null)
                text.SetActive(false);
        }
    }
}
