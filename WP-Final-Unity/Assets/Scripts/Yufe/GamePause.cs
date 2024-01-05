using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;
    [SerializeField] Button ResumeButton;
    [SerializeField] Button MenuButton;
    [SerializeField] Slider VolumeSlider;

    private bool Pause = false;
    private float Volume = 1f;
    
    void Start()
    {
        PauseUI.SetActive(false);
        ResumeButton.onClick.AddListener(Resume);
        MenuButton.onClick.AddListener(BackToMenu);
    }

    void Update()
    {
        ChangeVolume();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause = !Pause;
        }

        if(Pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
        }  
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
            
    }

    void ChangeVolume()
    {
        Volume = VolumeSlider.value;

        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = Volume;
        }
    }

    void Resume()
    {
        Pause = false;
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
