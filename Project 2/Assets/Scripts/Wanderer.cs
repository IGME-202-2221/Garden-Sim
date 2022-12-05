using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    public enum AgentStates
    {
        Wander,
        SeekFlower
    }

    Vector3 wanderForce;
    Vector3 boundsForce;
    Vector2 worldSize;

    [SerializeField]
    float boundsScalar = 1;

    public AgentStates currentState; // defaults to zero, or "Wander"
                                     // stores the index of agent's target, if there is one
    int targetIndex = 0;

    // temporarily use awake so we don't override the parent class start()
    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;

        // shrink world size slightly
        //worldSize *= 0.8f; -necessary if we are not using future position to calculate in bounds
    }

    public override void CalcSteeringForce()
    {
        // state-specific steering forces
        switch (currentState)
        { 
            case AgentStates.Wander:
                wanderForce = Wander();
                totalSteeringForce += wanderForce;

                // check for flowers in sight
                for (int i = 0; i < AgentManager.Instance.flowers.Count; i++)
                {
                    if ((AgentManager.Instance.flowers[i].transform.position - transform.position).magnitude <= sightRadius)
                    {
                        // if the agent "spots" a flower within its sight radius, change to the seeking state
                        targetIndex = i;
                        currentState = AgentStates.SeekFlower;
                    }
                }
                
                break;

            case AgentStates.SeekFlower:
                // seek flower
                totalSteeringForce += Seek(AgentManager.Instance.flowers[targetIndex].transform.position);

                // seek flower until flower has been pollinated (collision with an agent
                // if collision, reset target to null and switch to wander state

                break;
        }

        // agents should always stay in bounds, separate, and avoid obstacles regardless of state

        // bounds force
        // store force before adding it in order to draw gizmo
        boundsForce = StayinBounds(worldSize, 1f);
        totalSteeringForce += boundsForce * boundsScalar; // multiply by bounds scalar to increase the strength of the bounds force

        // have wanderers separation from each other
        totalSteeringForce += Separate(AgentManager.Instance.AgentsList);

        totalSteeringForce += AvoidObstacle(2f);
    }

    // changes from the currentState to a new state
    public void ChangeStateTo(AgentStates newState)
    {
        switch (newState)
        {
            case AgentStates.Wander:
                break;

            case AgentStates.SeekFlower:
                break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        // draw line to each obstacle
        Gizmos.color = Color.red;
        //    foreach(Vector3 pos in tempObPos)
        //    {
        //        Gizmos.DrawLine(physicsObject.Position, pos);
        //    }

        //    // draw avoidance zone box
        //    Vector3 futurePos = CalcFuturePosition(avoidMaxRange);
        //    float avoidMaxDist = Vector3.Magnitude(futurePos - physicsObject.Position);
        //    avoidMaxDist += avoidRadius;

        //    Gizmos.color = Color.blue;
        //    Gizmos.matrix = transform.localToWorldMatrix;
        //    Gizmos.DrawWireCube(new Vector3(0, avoidMaxDist / 2f, 0), 
        //                        new Vector3(avoidRadius * 2f, avoidMaxDist, avoidRadius));
        //    Gizmos.matrix = Matrix4x4.identity;

        // draw sight radius for agents
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }
}
