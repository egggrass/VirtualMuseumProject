using GLTFast.Schema;
using UnityEngine;

public class ShowImage : MonoBehaviour
{
    public GameObject showImage;
    public GameObject hideImage;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (showImage != null)
                showImage.SetActive(true);

            if (hideImage != null)
                hideImage.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }

}
