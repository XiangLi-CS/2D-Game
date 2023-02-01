using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   // public GameObject itemFeedback;             //Set up itemFeedback as a prefab

    private Animator animator;
    private AudioSource soundeffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        soundeffect = GetComponent<AudioSource>();

    }

    //item(collider) will disappear when player(collider) hits them
    private void OnTriggerEnter2D(Collider2D collision)
    {

        ItemEffect();
        
    }

    //Special Effects for Item
    private void ItemEffect()
    {
        animator.SetTrigger("Effect");
        soundeffect.Play();
        
    }

    private void Destory()
    {
        Destroy(gameObject);
    }


}
