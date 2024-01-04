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
    public Transform BossTrapDoor;
    public Door1Script Door1;
    public Door2Script Door2;
    public GameObject Boss;

    private string npcDialogue;
    private float ChatCount = 0f;
    private bool ActivePlate = false;
    private bool BossSpawning = false;
    private float OpenBossDoorCount = 0f;
    

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
                npcDialogue = "That Also Spawn More Enemys...";
                dialogueText.text = npcDialogue;
                SpawnBoss();
            }
        }

        if(BossSpawning)
        {
            float distanceToMove = -2f * Time.deltaTime;
            //BossTrapDoor.Translate(Vector3.right * distanceToMove);       //This was made for dragon 

            OpenBossDoorCount += Time.deltaTime;
            if(OpenBossDoorCount > 5f)
            {
                BossSpawning = false;
                Boss.SetActive(true);
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

    private void SpawnBoss()
    {
        BossSpawning = true;
    }
}
