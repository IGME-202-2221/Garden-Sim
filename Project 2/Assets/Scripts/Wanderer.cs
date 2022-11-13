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
    }
}