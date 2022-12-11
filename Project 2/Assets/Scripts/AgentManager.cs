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

    [SerializeField]
    int numBeetles;

    [SerializeField]
    public GameObject beetlePrefab;

    public List<Obstacle> obstacles = new List<Obstacle>();

    public List<GameObject> flowers = new List<GameObject>();

    public List<Agent> beetles = new List<Agent>();

    private float totalCamHeight;
    private float totalCamWidth;

    // get a reference to player that other scripts can utilize
    [SerializeField]
    GameObject player;
    public GameObject Player { get { return player; } }

    float score;
    public float Score { get { return score; } set { score = value; } }

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

        // instantiate a random number of butterfly agents
        int numButterflies = Random.Range(1, 6);
        for (int i = 0; i < numButterflies; i++)
        {
            GameObject newButt = Instantiate(prefabsList[1]);
            newButt.GetComponent<Transform>().position = new Vector3(Random.Range(-totalCamWidth, totalCamWidth), Random.Range(-totalCamHeight, totalCamHeight), 0);
            newButt.GetComponent<PhysicsObject>().Direction = Random.insideUnitCircle.normalized;

            agentsList.Add(newButt.GetComponent<Agent>());
        }

        // instantiate beetle agents at random locations with the bounds of the game
        for (int i = 0; i < numBeetles; i++)
        {
            GameObject newBeetle = Instantiate(beetlePrefab);
            newBeetle.GetComponent<Transform>().position = new Vector3(Random.Range(-totalCamWidth, totalCamWidth), Random.Range(-totalCamHeight, totalCamHeight), 0);

            beetles.Add(newBeetle.GetComponent<Agent>());
        }
    }
}
