using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeaponScript : MonoBehaviour
{
    [SerializeField] Collider hitbox;
    [SerializeField] GameObject hitAnimation;
    [SerializeField] float damage;
    GameObject player;
    Character character;
    SkeletonScript skeleton;
    // Start is called before the first frame update
    void Start()
    {
        skeleton = GetComponent<SkeletonScript>();
        player = skeleton.GetPlayer();
        character = player.GetComponent<Character>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 point1 = other.ClosestPointOnBounds(hitbox.transform.position);
            Vector3 point2 = hitbox.ClosestPointOnBounds(other.transform.position);
            Vector3 intersection = (point1 + point2) / 2f;
            ParticleSystem particle = Instantiate(hitAnimation).GetComponent<ParticleSystem>();
            particle.transform.position = intersection;
            particle.Play();
            character.Hurt(damage);
        }
    }
}
