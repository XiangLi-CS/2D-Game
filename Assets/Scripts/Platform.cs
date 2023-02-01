using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pos1, pos2;

    public float speed;

    public Transform startPos;

    Vector3 nextPos;

    private void Start()
    {
        nextPos = startPos.position;
    }

    private void Update()
    {
        //Checking next moving position
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }

        if(transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        //Moving platform
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }



}
