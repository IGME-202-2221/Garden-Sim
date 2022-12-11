using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Agent
{
    public enum AgentStates
    {
        Idle,
        SeekPlayer
    }

    Vector3 boundsForce;
    Vector2 worldSize;

    [SerializeField]
    float boundsScalar = 1;

    public AgentStates currentState; // defaults to zero, or "idle"

    // variables for controlling the speed at which beetles transition
    [SerializeField]
    float idleTime = 6f;
    float currentCountdown;

    private void Awake()
    {
        worldSize.y = Camera.main.orthographicSize;
        worldSize.x = Camera.main.aspect * worldSize.y;
    }

    public override void CalcSteeringForce()
    {
        // state-specific steering forces
        switch (currentState)
        {
            case AgentStates.Idle:
                // have beetle turn slightly red for duration of idle state
                GetComponent<SpriteRenderer>().color = Color.red;

                // look toward player direction, but remain idle
                transform.rotation = Quaternion.LookRotation(Vector3.back, AgentManager.Instance.Player.GetComponent<Transform>().position - physicsObject.Position);

                // increment current countdown time
                currentCountdown += Time.deltaTime;

                // check if enough time has elapsed to end the countdown
                if (currentCountdown >= idleTime)
                {
                    currentState = AgentStates.SeekPlayer;
                }
                break;


            case AgentStates.SeekPlayer:
                // turn beetles back to white
                GetComponent<SpriteRenderer>().color = Color.white;

                totalSteeringForce += Seek(AgentManager.Instance.Player.GetComponent<Transform>().position);

                // bounds force
                // store force before adding it in order to draw gizmo
                boundsForce = StayinBounds(worldSize, 1f);
                totalSteeringForce += boundsForce * boundsScalar; // multiply by bounds scalar to increase the strength of the bounds force

                // have beetles separate from each other
                totalSteeringForce += Separate(AgentManager.Instance.beetles);

                totalSteeringForce += AvoidObstacle(2f);

                // once beetle is moving, check for collisions with bees and butterflies
                for (int i = 0; i < AgentManager.Instance.agentsList.Count; i++)
                {
                    if (CircleCollision(gameObject, AgentManager.Instance.agentsList[i].GetComponent<Agent>()))
                    {
                        // set agent's collision to true
                        AgentManager.Instance.agentsList[i].GetComponent<Agent>().BeetleCollision = true;

                        // destroy beetle
                        AgentManager.Instance.beetles.Remove(gameObject.GetComponent<Agent>());
                        Destroy(gameObject);

                        // spawn in a new beetle at a random location
                        GameObject newBeetle = Instantiate(AgentManager.Instance.beetlePrefab);
                        newBeetle.GetComponent<Transform>().position = new Vector3(Random.Range(-totalCamWidth, totalCamWidth), Random.Range(-totalCamHeight, totalCamHeight), 0);

                        AgentManager.Instance.beetles.Add(newBeetle.GetComponent<Agent>());
                    }
                }

                break;
        }
    }
}
