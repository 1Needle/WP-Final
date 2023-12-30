using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenserScript : MonoBehaviour
{
    private bool Active = false;
    public GameObject player;
    public NPC2Script NPC2;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1.3f))
        {
            if (hit.collider.gameObject == player)
            {
                if (!Active)
                {
                    Active = true;
                    //Debug.Log("Player is on the plate!");
                    NPC2.OpenDoor();
                }
            }
        }
    }
}
