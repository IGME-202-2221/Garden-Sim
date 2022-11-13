using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    // singleton static instance
    public static AgentManager Instance;

    // list of possible prefabs to be spawned
    [SerializeField]
    List<GameObject> prefabsList;

    // stores all active agents
    public List<Agent> agentsList = new List<Agent>();

    // property to access list of agents
    public List<Agent> AgentsList { get { return agentsList; } }

    [SerializeField]
    int numBees;

    private float totalCamHeight;
    private float totalCamWidth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        totalCamHeight = Camera.main.orthographicSize;
        totalCamWidth = totalCamHeight * Camera.main.aspect;

        // instantiate a bunch of bee entities at random locations, with random directions to wander
        for (int i = 0; i < numBees; i++)
        {
            // create, and set random location within camera and random direction
            GameObject newBee = Instantiate(prefabsList[0]);
            newBee.GetComponent<Transform>().position = new Vector3(Random.Range(-totalCamWidth, totalCamWidth), Random.Range(-totalCamHeight, totalCamHeight), 0);
            newBee.GetComponent<PhysicsObject>().Direction = Random.insideUnitCircle.normalized;

            agentsList.Add(newBee.GetComponent<Agent>());
        }
    }
}
