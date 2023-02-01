using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehaviorManager
{
    public FlockBehaviorManager[] behaviors;       //behaviors that will be compositing together
    public float[] weights;

    //Inheriting from FlockBehavior
    //Combine all the flocking behavior
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //Check data whether is matched or not 
        if(weights.Length != behaviors.Length)
        {
            Debug.LogError("Data mismatch in" + name, this);
            return Vector2.zero;
        }

        //Movement
        Vector2 move = Vector2.zero;

        //Repeat each behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

            if(partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }

   

    
}
