using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds the information each specific flower will hold
public class Flower : MonoBehaviour
{
    // provides a wait time before a flower is destroyed
    [SerializeField]
    float waitTime = 5f;
    float currentTime = 0f;

    bool hasCollided = false;
    public bool HasCollided { get { return hasCollided; } set { hasCollided = value; } }

    // Update is called once per frame
    void Update()
    {
        // if there has been a collision, increment timer
        if (hasCollided)
        {
            // increment currentTime
            currentTime += Time.deltaTime;

            if (currentTime >= waitTime)
            {
                // reset timer
                currentTime = 0f;

                // remove flower from list
                AgentManager.Instance.flowers.Remove(gameObject);

                // destroy this flower
                Destroy(gameObject);
            }

        }
    }
}
