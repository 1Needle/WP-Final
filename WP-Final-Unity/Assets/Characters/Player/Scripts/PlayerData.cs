using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Character
{
    [SerializeField] PlayerController playerController;
    [SerializeField] AudioController audioController;

    private float hp = 100f;
    private float dmg = 15f;
    float hurt_dmg = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"hp: {hp}");

        if (hp <= 0)
        {
            Die();
        }
        if(get_Firedmg == true)
        {
            Fire_FullLastTime();
            Fire_secdmg();
        }
    }

    //timer
    private float Fire_timer = 0f;  // 計時器變數
    private float Fire_duration = 1f;  // 計時的總時間
    private void Fire_secdmg()
    {
        // 更新計時器
        Fire_timer += Time.deltaTime;

        // 檢查是否超過了指定的時間
        if (Fire_timer >= Fire_duration)
        {
            hp -= hurt_dmg;

            // 重置計時器
            Fire_timer = 0f;
        }
    }
    private float FireFLT_timer = 0f;  // 計時器變數
    private float FireFLT_duration;  // 計時的總時間
    private void Fire_FullLastTime()
    {
        // 更新計時器
        FireFLT_timer += Time.deltaTime;

        // 檢查是否超過了指定的時間
        if (FireFLT_timer >= FireFLT_duration)
        {
            get_Firedmg = false;

            // 重置計時器
            FireFLT_timer = 0f;
            Fire_timer = 0f;
        }
    }


    //Player Events
    private void Die()
    {
        playerController.Receive_Die(true);
    }

    public override void Hurt(float damage)
    {
        if(playerController.Get_Defending() == true)
        {
            audioController.Receive_Audio_Defending_GetHit();
            return;
        }

        playerController.Receive_Gethit(true);
        hp -= damage;
    }

    bool get_Firedmg = false;
    public override void Fire_Hurt(float damage, float last_time)
    {
        get_Firedmg = true;

        FireFLT_duration = last_time;
        hurt_dmg = damage;
    }

    public void PlayerIsDizzy(float get_damage)
    {
        hp -= get_damage;

        playerController.Receive_Dizzy(true);
    }




    //========================================================
    //====================Global Get Value====================
    //========================================================
    public float Get_dmg()
    {
        return dmg;
    }
    public void Receive_dmg(string receive_dmg)
    {
        if(receive_dmg == "OnFire")
        {
            dmg = 25;
        }
        else
        {
            dmg = 15;
        }
    }

    public void Get_Heal(float get_Heal)
    {
        if(hp <= 100f)
        {
            hp += get_Heal;
        }
    }
}
