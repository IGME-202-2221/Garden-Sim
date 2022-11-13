using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    // fields
    [SerializeField]
    SpriteRenderer sprite;

    // stores the min and max bounds
    public float MaxX { get { return sprite.bounds.max.x; } }
    public float MinX { get { return sprite.bounds.min.x; } }

    public float MaxY { get { return sprite.bounds.max.y; } }
    public float MinY { get { return sprite.bounds.min.y; } }

    // have radius variable
    private float radius;
    public float Radius { get { return radius; } }

    void Start()
    {
        Init();
    }

    void Init()
    {
        // calculate radius here
        Vector3 radiusVect = sprite.bounds.max - sprite.bounds.center;
        radius = radiusVect.magnitude;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sprite.bounds.center, radius);
    }
}
