using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay in area")]
public class StayInCertainAreaBehavior : FlockBehaviorManager
{
    public Vector2 center;
    public float radius = 10f;

    //Inheriting from FlockBehavior
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Calculate how far the agents move and when they come back 
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        //How close to the centre
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
