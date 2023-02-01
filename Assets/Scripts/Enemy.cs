using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource death;

    //Inheritance
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        death = GetComponent<AudioSource>();

    }

    public void Death()
    {
        Destroy(this.gameObject);
        
    }

    //Special Effect for enemy death
    public void DeathEffect()
    {
        animator.SetTrigger("Death");
        death.Play();


    }

    
}
