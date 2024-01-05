using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bruh : MonoBehaviour
{
    [SerializeField] DragonScripts dragon;
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger");
        if(other.transform.root.CompareTag("Enemy"))
            dragon.Return();
    }
}
