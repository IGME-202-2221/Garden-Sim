using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // fill this list with any entities or prefabs that will be spawned
    [SerializeField]
    List<GameObject> entitiesList;

    [SerializeField]
    int numBees;

    protected float totalCamHeight;
    protected float totalCamWidth;

    // Start is called before the first frame update
    void Start()
    {
        totalCamHeight = Camera.main.orthographicSize;
        totalCamWidth = totalCamHeight * Camera.main.aspect;

        // instantiate a bunch of bee entities at random locations, with random directions to wander
        for (int i = 0; i <= numBees; i++)
        {
            // create, and set random location within camera and random direction
            GameObject newBee = Instantiate(entitiesList[0]);
            newBee.GetComponent<Transform>().position = new Vector3(Random.Range(-totalCamWidth, totalCamWidth), Random.Range(-totalCamHeight, totalCamHeight), 0);
            newBee.GetComponent<PhysicsObject>().Direction = Random.insideUnitCircle.normalized;
        }
    }
}
