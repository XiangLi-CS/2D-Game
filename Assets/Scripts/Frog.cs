using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField]
    private float leftNode;
    [SerializeField]
    private float rightNode;

    [SerializeField]
    private float jumpLength;
    [SerializeField]
    private float jumpHeight;

    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private BoxCollider2D box;

    private Rigidbody2D r;

    private bool facingLeft = true;

    private Animator animator;

    //Managing State
    public enum FrogFSM
    {
        idle,
        jumping,
        falling,
    }

    private FrogFSM state = FrogFSM.idle;

    private void Start()
    {
        r = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        AnimationState();
        animator.SetInteger("State", (int)state);

    }

    //Frog FSM (managing state)
    private void AnimationState()
    {
        if (state == FrogFSM.jumping)
        {
            if (r.velocity.y < 0.1f)
            {
                state = FrogFSM.falling;
            }
        }
        else if (state == FrogFSM.falling)
        {
            if (box.IsTouchingLayers(ground))
            {
                state = FrogFSM.idle;
            }
        }
        else
        {
            state = FrogFSM.idle;
        }
    }

    //Frog Movement 
    private void Movement()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftNode)
            {
                if (transform.localScale.x != 5)
                {
                    transform.localScale = new Vector3(5, 5);
                }
                if (box.IsTouchingLayers(ground))
                {
                    r.velocity = new Vector2(-jumpLength, jumpHeight);
                    state = FrogFSM.jumping;
                }

            }
            else
            {
                facingLeft = false;
            }

        }
        else
        {
            if (transform.position.x < rightNode)
            {
                if (transform.localScale.x != -5)
                {
                    transform.localScale = new Vector3(-5, 5);
                }
                if (box.IsTouchingLayers(ground))
                {
                    r.velocity = new Vector2(jumpLength, jumpHeight);
                    state = FrogFSM.jumping;
                }

            }
            else
            {
                facingLeft = true;
            }
        }
    }
}
