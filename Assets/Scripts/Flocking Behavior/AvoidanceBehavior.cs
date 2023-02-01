using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehaviorManager
{
    //Inheriting from FlockBehavior
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Check whether these is neighbor or not, if there are no neighbors, return null
        if (context.Count == 0)
        {
            return Vector2.zero;
        }


        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if(Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                //Moving away from the neighbor
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);

            }
            
        }

        if(nAvoid > 0)
        {
            avoidanceMove /= nAvoid;
        }

        return avoidanceMove;
    }
}
