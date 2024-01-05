using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpawner : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Enemy;
    [SerializeField] Collider collider;

    private bool BossSpawn = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (BossSpawn == false)
            {
                Debug.Log("Enter Boss Room");
                BossSpawn = true;
                Boss.SetActive(true);
                Enemy.SetActive(false);
                collider.enabled = false;
            }

        }
    }
}
