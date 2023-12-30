using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCScript : MonoBehaviour
{
    public Transform player;
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    private float chatDistance = 5f;
    private string npcDialogue = "Press 'R' To Start Your Journey";
    public Door1Script Door1;
    public Door2Script Door2;
    private bool OpenTheDoor = false;

    private void Start()
    {
        npcDialogue = "Press 'R' To Start Your Journey";
        dialogueCanvas.enabled = false;
        //Door1 = GameObject.FindObjectOfType<Door1Script>();
        //Door2 = GameObject.FindObjectOfType<Door2Script>();
    }

    private void Update()
    {
        //float moveAmount = 10f * Time.deltaTime;
        //transform.Translate(Vector3.forward * moveAmount);

        // Rotate the NPC to face the player
        Vector3 directionToPlayer = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);

        // chat if within distance
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chatDistance)
        {
            dialogueCanvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.R) && (OpenTheDoor == false))
            {
                npcDialogue = "Have Fun";
                dialogueText.text = npcDialogue;
                Door1.Open();
                Door2.Open();
            }
        }
        else
        {
            dialogueCanvas.enabled = false;
        }

    }
    
}
