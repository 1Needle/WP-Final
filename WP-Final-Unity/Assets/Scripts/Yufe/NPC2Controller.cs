using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2Controller : MonoBehaviour
{
    private int KilledEnemycount = 0;
    [SerializeField] int EnemyAmount = 5; // total amount in Clear Room
    [SerializeField] GameHandler gameHandler;

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
        gameHandler.OneKill();
    }
}

//To call
//public NPC2Controller nPC2Controller;
//nPC2Controller.EnemyDied();
