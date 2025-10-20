using UnityEngine;

public class PlushToy : MonoBehaviour,Interactable
{

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        animator.SetTrigger("Punch");
        Debug.Log("Punch");
    }
}
