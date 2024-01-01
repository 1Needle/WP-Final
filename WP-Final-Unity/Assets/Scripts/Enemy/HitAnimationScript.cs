using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitAnimationScript : MonoBehaviour
{
    [SerializeField] GameObject hitAnimation;
    new Collider collider;
    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    public void Play(Collider target)
    {
        Vector3 point1 = target.ClosestPointOnBounds(collider.transform.position);
        Vector3 point2 = collider.ClosestPointOnBounds(target.transform.position);
        Vector3 intersection = (point1 + point2) / 2f;
        Transform instant = Instantiate(hitAnimation).transform;
        instant.position = intersection;
    }
}
