using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : Character
{
    [SerializeField] PlayerController playerController;
    [SerializeField] AudioController audioController;

    [SerializeField] RawImage rawImage60;
    [SerializeField] RawImage rawImage30;

    private float hp = 100f;
    private float dmg = 15f;
    float hurt_dmg = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        rawImage30.enabled = false;
        rawImage60.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Player hp: {hp}");

        if (hp <= 0)
        {
            Die();
        }
        if(get_Firedmg == true)
        {
            Fire_FullLastTime();
            Fire_secdmg();
        }

        if (hp <= 60 && hp >= 30)
        {
            rawImage60.enabled = true;

            rawImage30.enabled = false;
        }
        else if (hp <= 30)
        {
            rawImage30.enabled = true;

            rawImage60.enabled = false;
        }
        else
        {
            rawImage30.enabled = false;
            rawImage60.enabled = false;
        }
    }

    //timer
    private float Fire_timer = 0f;  // �p�ɾ��ܼ�
    private float Fire_duration = 1f;  // �p�ɪ��`�ɶ�
    private void Fire_secdmg()
    {
        // ��s�p�ɾ�
        Fire_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (Fire_timer >= Fire_duration)
        {
            hp -= hurt_dmg;

            // ���m�p�ɾ�
            Fire_timer = 0f;
        }
    }
    private float FireFLT_timer = 0f;  // �p�ɾ��ܼ�
    private float FireFLT_duration;  // �p�ɪ��`�ɶ�
    private void Fire_FullLastTime()
    {
        // ��s�p�ɾ�
        FireFLT_timer += Time.deltaTime;

        // �ˬd�O�_�W�L�F���w���ɶ�
        if (FireFLT_timer >= FireFLT_duration)
        {
            get_Firedmg = false;

            // ���m�p�ɾ�
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

        Debug.Log($"Enemy->Player: {damage}");

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
