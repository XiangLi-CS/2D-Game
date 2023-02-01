using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviorManager : ScriptableObject
{
    //Inheritance
    //Managing different flocking behaviors
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);

}
