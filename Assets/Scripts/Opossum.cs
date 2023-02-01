using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
    [SerializeField]
    private float speed;

    public Transform[] moveNode;
    private int randomNode;

    //Inheriting from Enemy
    protected override void Start()
    {
        base.Start();
        randomNode = Random.Range(0, moveNode.Length);
        
    }
    private void Update()
    {
        //Flip ground enemy once they attached the nodes
        if(moveNode[randomNode].transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 1);
        }
        else
        {
            transform.localScale = new Vector3(5, 5, 1);
        }

        //Ground enemy movement
        transform.position = Vector2.MoveTowards(transform.position, moveNode[randomNode].position, speed * Time.deltaTime);

        //Once enemy attached the next node, return to previous node
        if (Vector2.Distance(transform.position, moveNode[randomNode].position) < 0.2f)
        {

            randomNode = Random.Range(0, moveNode.Length);
            
        }
        
    }

    

}
