using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HitAnimationScript))]
public class ClawAttackScript : MonoBehaviour
{
    HitAnimationScript hitAnimationScript;
    Character player;
    static bool attacked;
    float damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        hitAnimationScript = GetComponent<HitAnimationScript>();
    }

    public void StartClawAttack(float dmg)
    {
        attacked = false;
        damage = dmg;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!attacked)
        {
            if (other.CompareTag("Player"))
            {
                attacked = true;
                player.Hurt(damage);
                hitAnimationScript.Play(other);
            }
        }
    }
}
