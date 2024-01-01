using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitAnimationScript))]
public class MagicAttackScript : MonoBehaviour
{
    [SerializeField] float hitboxStartTime, hitboxDuration;
    HitAnimationScript hitAnimationScript;
    static float damage;
    static bool attacked;
    PlayerData player;
    new Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        hitAnimationScript = GetComponent<HitAnimationScript>();
        collider = GetComponent<Collider>();
        Invoke(nameof(EnableCollider), hitboxStartTime);
    }

    public void StartMagicAttack(float dmg)
    {
        damage = dmg;
        attacked = false;
    }

    void EnableCollider()
    {
        collider.enabled = true;
        Invoke(nameof(DisableCollider), hitboxDuration);
    }

    void DisableCollider()
    {
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!attacked)
        {
            if (other.CompareTag("Player"))
            {
                hitAnimationScript.Play(other);
                player.PlayerIsDizzy(damage);
                attacked = true;
            }
        }
    }
}
