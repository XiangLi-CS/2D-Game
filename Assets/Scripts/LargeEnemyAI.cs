using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/*A customized Enemy AI function base on the AstarPackage*/
public class LargeEnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D r;

    private void Start()
    {
        
        seeker = GetComponent<Seeker>();
        r = GetComponent<Rigidbody2D>();

        //Keep finding path
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    //Draw Path to the target
    void UpdatePath()
    {
        //Creating Path and keep generating Path
        if (seeker.IsDone())
        {
            seeker.StartPath(r.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        //Checking path error
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        //Checking whether there is path or not
        if (path == null)
        {
            return;
        }

        //Checking whether it reach to the destination or not
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //Move to next position
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - r.position).normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        r.AddForce(force);

        float distance = Vector2.Distance(r.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Flip enemy
        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}