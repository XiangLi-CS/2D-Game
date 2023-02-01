using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohensionBehavior : FlockBehaviorManager
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    //Inheriting from FlockBehavior
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Check whether these is neighbor or not, if there are no neighbors, return null
        if (context.Count == 0)
        {
            return Vector2.zero;
        }


        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            //Moving towards to the neighbor
            cohesionMove += (Vector2)item.position;
        }

        cohesionMove /= context.Count;

        //Reset agent
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }
}
