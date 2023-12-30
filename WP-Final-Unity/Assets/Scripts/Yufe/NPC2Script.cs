using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC2Script : MonoBehaviour
{
    public Transform player;
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    private string npcDialogue;
    public Door1Script Door1;
    public Door2Script Door2;
    private float ChatCount = 0f;
    private bool ActivePlate = false;

    private void Start()
    {
        npcDialogue = "Stand On Plate To Open The Door";
        dialogueText.text = npcDialogue;
        //dialogueCanvas.enabled = false;
        //Door1 = GameObject.FindObjectOfType<Door1Script>();
        //Door2 = GameObject.FindObjectOfType<Door2Script>();
    }

    private void Update()
    {

        // Rotate the NPC to face the player
        Vector3 directionToPlayer = player.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);

        if (ActivePlate == true)
        {
            ChatCount += Time.deltaTime;
            if (ChatCount > 5f)
            {
                ActivePlate = false;
                npcDialogue = "That Also Spawn The Boss...";
                dialogueText.text = npcDialogue;
                // SpawnBoss();
            }
        }

    }
    public void OpenDoor()
    {
        ActivePlate = true;
        npcDialogue = "I forgot to tell you...";
        dialogueText.text = npcDialogue;
        Door1.Open();
        Door2.Open();
    }
}
