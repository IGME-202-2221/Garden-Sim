using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Agent
{
    public enum AgentStates
    {
        Wander,
        SeekFlower
    }

    Vector3 boundsForce;
    Vector2 worldSize;

    [SerializeField]
    float boundsScalar = 1;

    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;
    }

    public override void CalcSteeringForce()
    {
        // finite state machine allowing beetles to transition from idle state

        totalSteeringForce += Seek(AgentManager.Instance.Player.GetComponent<Transform>().position);

        // beetles should always stay in bounds, separate, and avoid obstacles regardless of state

        // bounds force
        // store force before adding it in order to draw gizmo
        boundsForce = StayinBounds(worldSize, 1f);
        totalSteeringForce += boundsForce * boundsScalar; // multiply by bounds scalar to increase the strength of the bounds force

        // have beetles separate from each other
        totalSteeringForce += Separate(AgentManager.Instance.beetles);

        totalSteeringForce += AvoidObstacle(2f);
    }
}
