using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementConstraints : MonoBehaviour
{
    [SerializeField] DragonScripts dragon;
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Enemy"))
            dragon.Return();
    }
}
