using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] TextMeshProUGUI W_L;
    [SerializeField] TextMeshProUGUI PlayTime;
    [SerializeField] TextMeshProUGUI EnemyKilled;

    private float PlayTimeCount = 0f;
    private bool GameEnd = false;
    private int EnemyKilledCount = 0;
    private bool WarpPlayer = false;
    private float WarpCount = 0f;
    private Vector3 WarpPosition = new Vector3(95f, 2.2f, 225f);

    // Update is called once per frame
    void Update()
    {
        if(GameEnd == false)
        {
            PlayTimeCount += Time.deltaTime;
        }

        if(WarpPlayer == true)
        {
            WarpCount += Time.deltaTime;
            if(WarpCount > 5f)
            {
                WarpPlayer = false;
                Player.transform.position = WarpPosition;
            }
        }
    }

    public void OneKill()
    { 
        EnemyKilledCount++; 
    }

    public void GameFinised(bool WIN)
    {
        if (GameEnd == true) return;

        GameEnd = true;

        PlayTime.text = "Your PlayTime: " + PlayTimeCount/3600 + " : " + (PlayTimeCount % 3600) / 60 + " : " + PlayTimeCount % 60;
        EnemyKilled.text = "Enemy Killed: " + EnemyKilledCount;

        if (WIN == false)
            W_L.text = "You Win :)";
        else
            W_L.text = "You Lose :( ";
    }


}
