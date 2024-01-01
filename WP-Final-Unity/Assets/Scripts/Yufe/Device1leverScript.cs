using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Device1leverScript : MonoBehaviour
{
    public Transform player;
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    private string npcDialogue;
    public Transform lever;
    private bool Actived = false;
    private float InteractDistance = 6f;

    private void Start()
    {
        npcDialogue = "Press'R'";
        dialogueText.text = npcDialogue;
    }

    private void Update()
    {

        // Rotate the NPC to face the player
        //Vector3 directionToPlayer = player.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= InteractDistance)
        {
            if (Input.GetKeyDown(KeyCode.R) && (Actived == false))
            {
                Actived = true;
                npcDialogue = "Device Active";
                dialogueText.text = npcDialogue;

                // rotate lever
                Vector3 rotationAxis = Vector3.right;
                float rotationAngle = 60f;
                lever.RotateAround(transform.position, rotationAxis, rotationAngle);
            }
        }

    }
    
}


