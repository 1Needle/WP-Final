using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1Script : MonoBehaviour
{
    private float DoorMoveSpeed = 2f;
    private bool OpenTheDoor = false;
    private float OpenDoorClock = 0f;
    private Collider myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider>();
    }
    
    void Update()
    {
        
        if (OpenTheDoor)
        {
            if (OpenDoorClock <= 6f)
            {
                OpenDoorClock += Time.deltaTime;
                float moveAmount = DoorMoveSpeed * Time.deltaTime;
                transform.Translate(Vector3.right * moveAmount);
            }
        }
    }

    public void Open()
    {
        //myCollider.enabled = !myCollider.enabled;
        OpenTheDoor = true;
    }
}
