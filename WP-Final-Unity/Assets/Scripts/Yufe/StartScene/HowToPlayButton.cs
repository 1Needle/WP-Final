using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject objectHowToPlay;

    void Start()
    {
        Button button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (objectHowToPlay != null)
        {
            objectHowToPlay.SetActive(!objectHowToPlay.activeSelf);
        }
    }
}
