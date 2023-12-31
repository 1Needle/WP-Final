using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    GameObject player;
    static bool attacked;
    float damage;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartAttack(float dmg)
    {
        attacked = false;
        damage = dmg;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!attacked)
        {
            if (other.CompareTag("Player"))
            {
                attacked = true;
                Debug.Log("hurt");
            }
        }
    }
}
