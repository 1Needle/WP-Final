using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAttack : MonoBehaviour
{
    Character player;
    bool attacked;
    float damage, duration;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
    }

    public void StartFlame(float dmg, float time)
    {
        attacked = false;
        damage = dmg;
        duration = time;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(!attacked)
        {
            if(other.CompareTag("Player"))
            {
                attacked = true;
                player.Fire_Hurt(damage, duration);
            }
        }
    }
}
