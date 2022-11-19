using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // need to know obstacle position and radius
    public float radius = 1f;

    public Vector3 Position
    {
        get { return transform.position; }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(Position, radius);
    }

}
