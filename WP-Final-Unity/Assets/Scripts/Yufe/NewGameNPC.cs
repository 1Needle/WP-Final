using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewGameNPC : MonoBehaviour
{
    public Transform player;
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    private float chatDistance = 5f;
    private string npcDialogue = "Press 'R' To Start A New Game";

    private void Start()
    {
        npcDialogue = "Press 'R' To Start A New Game";
        dialogueCanvas.enabled = false;
    }

    private void Update()
    {
        // Rotate the NPC to face the player
        Vector3 directionToPlayer = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chatDistance)
        {
            dialogueCanvas.enabled = true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("StartScene");
            }
        }
        else
        {
            dialogueCanvas.enabled = false;
        }

    }

}

