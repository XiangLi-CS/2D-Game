using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefeb;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviorManager behavior;

    [Range(10,100)]
    public int startingCount = 50;          //how many flockagents
    const float AgentDensity = 0.08f;       //Agent Density

    [Range(1f, 100f)]
    public float driveFactor = 10f;         //Making agents move faster
    [Range(1f, 100f)]
    public float maxSpeed = 5f;             //Maximun moving speed
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;     //Radius for neighbors (the area of neighbors)
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f; //Radius for avodiance

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        //Initialize flock, and create list of agents arount circle
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefeb,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 1f)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //Iterating flock agents
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //Call the move funtion
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    //Getting nearby objects
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
