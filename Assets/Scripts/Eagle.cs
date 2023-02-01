using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    [SerializeField]
    private GameObject gravityTarget;

    private Rigidbody2D r;

    //Inheriting from Enemy
    protected override void Start()
    {
        base.Start();
        r = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        //Applying force to fly enemy
        r.AddForce(GetForceVector(), ForceMode2D.Force);
    }

    //Make fly enemy move up and down
    Vector2 GetForceVector()
    {
        Vector2 dir = gravityTarget.transform.position - transform.position;
        return dir;
    }



}