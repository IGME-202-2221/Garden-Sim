using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    Vector3 velocity = Vector3.zero;
    Vector3 position = Vector3.zero;

    public Vector3 Direction { get { return direction; } set { direction = value; } }
    public Vector3 Velocity { get { return velocity; } }
    public Vector3 Position { get { return position; } set { position = value; } }

    Vector3 acceleration = Vector3.zero;

    [SerializeField]
    float mass = 1f;    // mass should be 1, since you can't divide by zero

    [SerializeField]
    bool useGravity = false;

    [SerializeField]
    Vector3 gravity = Vector3.down;

    [SerializeField]
    bool useFriction = false;

    [SerializeField]
    float frictCoeff = 1f;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // add forces here (before setting velocity)
        if (useGravity)
        {
            ApplyGravity();
        }

        if (useFriction)
        {
            ApplyFriction();
        }

        // Calculate the velocity for this frame - New
        velocity += acceleration * Time.deltaTime;

        position += velocity * Time.deltaTime;

        // Grab current direction from velocity  - New
        direction = velocity.normalized;

        transform.position = position;

        // rotate sprite based on direction
        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        // Zero out acceleration - New
        acceleration = Vector3.zero;
    }

    // allow other classes to add forces to our objects
    public void ApplyForce(Vector3 force)
    {
        // F = M * A
        // A = F / M
        acceleration += force / mass;
    }

    void ApplyGravity()
    {
        acceleration += gravity;
    }

    void ApplyFriction()
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * frictCoeff;
        ApplyForce(friction);
    }
}
