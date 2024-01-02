using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LastDoorControllerScript : MonoBehaviour
{
    public Canvas dialogueCanvas;
    public TextMeshProUGUI dialogueText;
    public Door1Script Door1;
    public Door2Script Door2;

    private int ActivedDeviceCount = 0;
    private string npcDialogue;
    private bool SelfDestroy = false;
    private float SelfDestroyCount = 0f;

    private void Start()
    {
        npcDialogue = "Device Actived 0 / 4";
        dialogueText.text = npcDialogue;
    }

    private void Update()
    {
        npcDialogue = "Device Actived " + ActivedDeviceCount + " / 4";
        dialogueText.text = npcDialogue;

        Debug.Log("ActivedDevice:" + ActivedDeviceCount);

        if (ActivedDeviceCount == 4)
        {
            OpenDoor();
            SelfDestroy = true;
        }

        if(SelfDestroy == true) 
        {
            SelfDestroyCount += Time.deltaTime;
            if(SelfDestroyCount > 6f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OpenDoor()
    {
        npcDialogue = "Door Opening";
        dialogueText.text = npcDialogue;
        Door1.Open();
        Door2.Open();
        Debug.Log("Door3 Opening");
    }

    public void DeviceActived()
    {
        ActivedDeviceCount++;
    }

}

