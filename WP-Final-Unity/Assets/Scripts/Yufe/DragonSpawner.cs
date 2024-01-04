using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject BossSample;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= 5f)
        {
            BossSample.SetActive(false);
            Boss.SetActive(true);
        }

        
    }
}
