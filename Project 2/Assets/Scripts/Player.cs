using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // variables controlling movement
    [SerializeField]
    float speed = 5f;

    Vector3 vehiclePosition = Vector3.zero;
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    float totalCamHeight;
    float totalCamWidth;

    [SerializeField]
    List<GameObject> flowPrefabsList;

    float cooldownDuration = 2f;
    float cooldownTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // makes sure vehicle starts whereever it was placed, and not at 0, 0, 0
        vehiclePosition = transform.position;

        // store camera dimensions
        totalCamHeight = Camera.main.orthographicSize;
        totalCamWidth = totalCamHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // velocity is direcion * speed * deltaTime
        // this is a vector with a direction and magnitude
        velocity = direction * speed * Time.deltaTime;

        // increment position based on velocity
        vehiclePosition += velocity;

        // here we will want to validate position to make sure the vehicle stays on screen
        if (vehiclePosition.x > totalCamWidth)
        {
            vehiclePosition.x = totalCamWidth;
        }
        if (vehiclePosition.x < -totalCamWidth)
        {
            vehiclePosition.x = -totalCamWidth;
        }

        if (vehiclePosition.y > totalCamHeight)
        {
            vehiclePosition.y = totalCamHeight;
        }
        if (vehiclePosition.y < -totalCamHeight)
        {
            vehiclePosition.y = -totalCamHeight;
        }

        // draw the new (validated) position
        transform.position = vehiclePosition;

        // increment cooldown time
        cooldownTime += Time.deltaTime;
    }

    // write a method to be called by another script (like an event)
    public void OnMove(InputAction.CallbackContext context)
    {
        // get the value from the context
        direction = context.ReadValue<Vector2>();

        // prevents snapping back to default orientatino
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }
    }

    public void OnPlant(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // only place flower if playter is not on cooldown
            if (cooldownTime >= cooldownDuration)
            {
                // get random flower prefab from list
                int flowerIndex = Random.Range(0, 2);
                GameObject newFlower = Instantiate(flowPrefabsList[flowerIndex]);
                newFlower.GetComponent<Transform>().position = transform.position;

                // add to list of flowers in scene
                AgentManager.Instance.flowers.Add(newFlower);

                // reset cooldown
                cooldownTime = 0;
            }
        }

        if (context.canceled)
        {

        }
    }


}
