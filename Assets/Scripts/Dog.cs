using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dog : MonoBehaviour
{
    private Animator animator;
    public GameObject starEffect;

    public AudioClip petSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeHappy()
    {
        animator.CrossFade("Comforted", 0);
        gameManager.instance.hasPetTheDog = true;
        gameManager.instance.SetContentIndex();
        Destroy(starEffect);
        gameManager.instance.PlaySound(petSound);
        Invoke("CanControlLuna", 1.75f);
        
    }


    public void CanControlLuna()
    {
       gameManager.instance.canControlLuna = true;
    }

}
