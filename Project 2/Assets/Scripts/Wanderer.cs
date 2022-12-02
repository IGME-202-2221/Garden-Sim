using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    Vector3 wanderForce;
    Vector3 boundsForce;
    Vector2 worldSize;

    [SerializeField]
    float boundsScalar = 1;

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
        wanderForce = Wander();
        totalSteeringForce += wanderForce;

        // bounds force
        // store force before adding it in order to draw gizmo
        boundsForce = StayinBounds(worldSize, 1f);
        totalSteeringForce += boundsForce * boundsScalar; // multiply by bounds scalar to increase the strength of the bounds force

        // have wanderers separation from each other
        totalSteeringForce += Separate(AgentManager.Instance.AgentsList);

        totalSteeringForce += AvoidObstacle(2f);

    }

    private void OnDrawGizmosSelected()
    {
        // draw line to each obstacle
        Gizmos.color = Color.red;
        foreach(Vector3 pos in tempObPos)
        {
            Gizmos.DrawLine(physicsObject.Position, pos);
        }

        // draw avoidance zone box
        Vector3 futurePos = CalcFuturePosition(avoidMaxRange);
        float avoidMaxDist = Vector3.Magnitude(futurePos - physicsObject.Position);
        avoidMaxDist += avoidRadius;

        Gizmos.color = Color.blue;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(new Vector3(0, avoidMaxDist / 2f, 0), 
                            new Vector3(avoidRadius * 2f, avoidMaxDist, avoidRadius));
        Gizmos.matrix = Matrix4x4.identity;
    }
}
