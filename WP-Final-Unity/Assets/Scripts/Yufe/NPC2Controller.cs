using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2Controller : MonoBehaviour
{
    private int KilledEnemycount = 0;
    [SerializeField] int EnemyAmount = 28; // total amount in Clear Room

    public GameObject NPC2;

    void Update()
    {
        if(KilledEnemycount == EnemyAmount)
        {
            NPC2.SetActive(true);
        }
    }

    public void EnemyDied()
    {
        KilledEnemycount++;
    }
}

//To call
//public NPC2Controller nPC2Controller;
//nPC2Controller.EnemyDied();