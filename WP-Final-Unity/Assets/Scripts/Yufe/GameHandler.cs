using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] TextMeshProUGUI W_L;
    [SerializeField] TextMeshProUGUI PlayTime;
    [SerializeField] TextMeshProUGUI EnemyKilled;
    [SerializeField] GameObject Ghost;
    [SerializeField] GameObject AudioMaker1; // dragon
    [SerializeField] GameObject AudioMaker2; // enemy

    private float PlayTimeCount = 0f;
    private bool GameEnd = false;
    private int EnemyKilledCount = 0;
    private bool WarpPlayer = false;
    private float WarpCount = 0f;

    void Update()
    {
        
        if (GameEnd == false)
        {
            PlayTimeCount += Time.deltaTime;
        }

        if(WarpPlayer == true)
        {
            WarpCount += Time.deltaTime;
            if(WarpCount > 5f)
            {
                WarpPlayer = false;
                Player.SetActive(false);
                Ghost.SetActive(true);
            }
        }
    }

    public void OneKill()
    { 
        EnemyKilledCount++; 
    }

    public void GameFinised(bool WIN)
    {
        Debug.Log("GameFinished");

        if (GameEnd == true) return;

        GameEnd = true;
        WarpPlayer=true;

        PlayTime.text = "PlayTime: " + (int)PlayTimeCount/3600 + " : " + ((int)PlayTimeCount % 3600) / 60 + " : " + (int)PlayTimeCount % 60;
        EnemyKilled.text = "Enemy Killed: " + EnemyKilledCount;

        if (WIN == true)
            W_L.text = "You Win :)";
        else
            W_L.text = "You Died :( ";

        //StopAllAudioSources();
        AudioMaker1.SetActive(false);
        AudioMaker2.SetActive(false);

    }

    private void StopAllAudioSources()
    {
        // Find all active AudioSources in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Stop each AudioSource
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
    }

}
