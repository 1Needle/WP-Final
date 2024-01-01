using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeaponScript : MonoBehaviour
{
    [SerializeField] Collider hitbox;
    [SerializeField] GameObject hitAnimation;
    [SerializeField] float damage;
    Character player;
    SkeletonScript skeleton;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<SkeletonScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Vector3 point1 = other.ClosestPointOnBounds(hitbox.transform.position);
            Vector3 point2 = hitbox.ClosestPointOnBounds(other.transform.position);
            Vector3 intersection = (point1 + point2) / 2f;
            ParticleSystem particle = Instantiate(hitAnimation).GetComponent<ParticleSystem>();
            particle.transform.position = intersection;
            particle.Play();
            player.Hurt(damage);
        }
    }
}
