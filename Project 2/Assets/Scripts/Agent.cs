using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// require component ensures we can get a phsyics object (it exists)
[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    // this class will store all the possible actions an agent will do
    // we will make child classes of agents that will make their own decisions

    [SerializeField]
    protected PhysicsObject physicsObject;

    [SerializeField]
    float maxSpeed = 1f;

    //[SerializeField]
    //float maxForce = 2f;

    protected Vector3 totalSteeringForce;

    // reference to what to flee from
    [SerializeField]
    protected GameObject target;

    protected float totalCamHeight;
    protected float totalCamWidth;

    // for demo
    [SerializeField]
    float time;
    [SerializeField]
    float radius;

    float angle = 90f;
    [SerializeField]
    float angleStepMax;

    [SerializeField]
    float personalSpace = 1f;

    [SerializeField]
    protected float avoidMaxRange = 5f;
    [SerializeField]
    protected float avoidRadius = 1f;

    // temporary variable for debugging purposes
    protected List<Vector3> tempObPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        physicsObject = GetComponent<PhysicsObject>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // store camera dimensions in update to account for changes in screen size while game is running
        totalCamHeight = Camera.main.orthographicSize;
        totalCamWidth = totalCamHeight * Camera.main.aspect;

        CalcSteeringForce(); // child will look at parent's implementation of update

        // limit total force
        totalSteeringForce = Vector3.ClampMagnitude(totalSteeringForce, maxSpeed);

        physicsObject.ApplyForce(totalSteeringForce);

        // must zero out forces
        totalSteeringForce = Vector3.zero;
    }

    // child classes have to implement this method
    public abstract void CalcSteeringForce();

    // protected so child classes can access
    // make sure to pass in a position in WORLD space, not local space
    // flee will be physicsObjet.position - target position
    protected Vector3 Seek(Vector3 targetPosition, float weight = 1f)
    {
        Vector3 desiredVelocity = targetPosition - physicsObject.Position;

        // get scaled vector
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // calculate turning force
        Vector3 seekForce = desiredVelocity - physicsObject.Velocity;

        return seekForce * weight;
    }

    protected Vector3 Flee(Vector3 targetPosition, float weight = 1f)
    {
        Vector3 desiredVelocity = physicsObject.Position - targetPosition;

        // get scaled vector
        desiredVelocity = desiredVelocity.normalized.normalized * maxSpeed;

        // calculate turning force
        Vector3 fleeForce = desiredVelocity - physicsObject.Velocity;

        return fleeForce * weight;
    }

    protected Vector3 Wander(float weight = 1f)
    {
        // find random (future) position
        Vector3 wanderPos = CalcFuturePosition(time);

        angle += Random.Range(-angleStepMax, angleStepMax);
        wanderPos.x += Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        wanderPos.y += Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        // seek that random position
        return Seek(wanderPos) * weight;

        // if we don't want agents to just patrol the outside,
        // you can push point on rotation circle toward the max/min (toward the center), or apply bounds scalar in wander
    }

    // need to know the bounds
    // we want a manager to calculate bounds, instead of each agent doing that
    // have this method look for data outside of itself (property?)
    protected Vector3 StayinBounds(Vector2 bounds, float time = 0) // optional parameter that defaults to time of zero, meaning current position, this will use current position to stay in bounds, not future
    {
        // get position
        Vector3 position = CalcFuturePosition(time);

        // check position
        // if out of bounds
        if (position.x > bounds.x ||
            position.x < -bounds.x ||
            position.y > bounds.y ||
            position.y < -bounds.y)
        {
            return Seek(Vector3.zero);
        }

        // else if in bounds
        return Vector3.zero;
    }

    // generic separate function, meaning that agents can separate from other child types
    protected Vector3 Separate(List<Agent> agents)
    {
        // sqaure to avoid having to take the sqaure root
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2f);

        // loop through all other agents
        foreach (Agent other in agents)
        {
            // get sqr distance between agents
            float sqrDist = Vector3.SqrMagnitude(other.physicsObject.Position - physicsObject.Position);

            // check to make sure agent is not looking at itself
            // in which case do nothing
            if (sqrDist < float.Epsilon)
            {
                continue; // do nothing and skip to next iteration
            }

            if (sqrDist < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDist + 0.1f);
                return Flee(other.physicsObject.Position, weight);
            }

        }
        return Vector3.zero;
    }

    public Vector3 CalcFuturePosition(float time)
    {
        return physicsObject.Position + (physicsObject.Direction * maxSpeed * time);
    }

    public Vector3 AvoidObstacle(float weight = 1f)
    {
        Vector3 avoidForce = Vector3.zero;
        Vector3 VtoO = Vector3.zero;                    // vector to obstacle
        Vector3 futurePos = CalcFuturePosition(avoidMaxRange);     // may want to store time value in wander or child class (depending on which agents require it)

        float dotForward, dotRight;
        float avoidMaxSqrDist = Vector3.SqrMagnitude(futurePos - physicsObject.Position);
        avoidMaxSqrDist += avoidRadius;

        // add all obstacle positions to the list
        tempObPos.Clear();
        foreach (Obstacle obstacle in AgentManager.Instance.obstacles)
        {
            // calculate vector to object
            VtoO = obstacle.Position - physicsObject.Position;

            // is this in front?
            dotForward = Vector3.Dot(VtoO, physicsObject.Velocity.normalized);     // left-hand vector will always be unnormalized

            if (dotForward > 0 && dotForward * dotForward < avoidMaxSqrDist)
            {
                dotRight = Vector3.Dot(VtoO, transform.right);

                // check if within avoidance zone rectangle
                if (Mathf.Abs(dotRight) < avoidRadius + obstacle.radius)
                {
                    tempObPos.Add(obstacle.Position);         //for gizmos

                    // steer agents away from obstacles
                    if (dotRight > 0)
                    {
                        // steer agent left
                        avoidForce += -transform.right * maxSpeed * (1f / dotForward);
                    }
                    else
                    {
                        // steer agent right
                        avoidForce += transform.right * maxSpeed * (1f / dotForward);
                    }
                }
            }
        }

        return avoidForce;
    }
}
