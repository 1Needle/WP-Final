using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource Walk_audioSource;
    [SerializeField] AudioSource Run_audioSource;
    [SerializeField] AudioSource Hit_audioSource;
    [SerializeField] AudioSource GetHit_audioSource;
    [SerializeField] AudioSource Defending_GetHit_audioSource;

    [SerializeField] AudioSource Skill_OnFire_audioSource;
    [SerializeField] AudioSource Skill_OnHeal_audioSource;
    [SerializeField] AudioSource Skill_Explosion_audioSource;
    [SerializeField] AudioSource Skill_Explosion_soundEF_audioSource;

    [SerializeField] AudioClip Walk;
    [SerializeField] AudioClip Run;
    [SerializeField] AudioClip Hit;
    [SerializeField] AudioClip GetHit;
    [SerializeField] AudioClip GetHit_DangDang;
    [SerializeField] AudioClip Defending_GetHit;

    [SerializeField] AudioClip Skill_OnFire;
    [SerializeField] AudioClip Skill_OnHeal;
    [SerializeField] AudioClip Skill_Explosion;
    [SerializeField] AudioClip Skill_Explosion_soundEF;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool Walking = false;
    public void Receive_Audio_Walk()
    {
        if (Walking == true)
        {
            return;
        }

        Debug.Log("AudioWalk");
        Walk_audioSource.volume = 0.7f;
        Walk_audioSource.clip = Walk;
        Walk_audioSource.loop = true;
        Walk_audioSource.Play();

        Walking = true;
    }

    private bool Running = false;
    public void Receive_Audio_Run()
    {
        if (Running == true)
        {
            return;
        }

        Run_audioSource.volume = 0.7f;
        Run_audioSource.pitch = Mathf.Clamp(1.3f, 0.1f, 3.0f);
        Run_audioSource.clip = Run;
        Run_audioSource.loop = true;
        Run_audioSource.Play();

        Running = true;
    }

    public void StopLooping_Walk()
    {
        Walk_audioSource.Play();

        Walking = false;
    }
    public void StopLooping_Run()
    {
        Run_audioSource.Stop();

        Running = false;
    }

    public void Receive_Audio_Hit()
    {
        Hit_audioSource.volume = 0.4f;
        Hit_audioSource.clip = Hit;
        Hit_audioSource.Play();
    }

    public void Receive_Audio_GetHit()
    {
        GetHit_audioSource.volume = 0.5f;

        int randomInt = Random.Range(1, 9);
        if(randomInt == 1 && Skill_OnFire_audioSource.isPlaying == false && Skill_OnHeal_audioSource.isPlaying == false)
        {
            GetHit_audioSource.clip = GetHit_DangDang;
        }
        else
        {
            GetHit_audioSource.clip = GetHit;
        }

        GetHit_audioSource.Play();
    }

    public void Receive_Audio_Defending_GetHit()
    {
        Defending_GetHit_audioSource.volume = 0.5f;
        Defending_GetHit_audioSource.clip = Defending_GetHit;
        Defending_GetHit_audioSource.Play();
    }

    public void Receive_Audio_Skill(string skill)
    {
        if (skill == "OnFire")
        {
            Skill_OnFire_audioSource.volume = 0.4f;
            Skill_OnFire_audioSource.clip = Skill_OnFire;

            Skill_OnFire_audioSource.Play();
            Skill_OnHeal_audioSource.Stop();
        }
        else if (skill == "OnHeal")
        {
            Skill_OnHeal_audioSource.volume = 0.4f;
            Skill_OnHeal_audioSource.clip = Skill_OnHeal;

            Skill_OnFire_audioSource.Stop();
            Skill_OnHeal_audioSource.Play();
        }
        else
        {
            Skill_Explosion_audioSource.volume = 0.4f;
            Skill_Explosion_audioSource.clip = Skill_Explosion;

            Skill_Explosion_soundEF_audioSource.volume = 0.4f;
            Skill_Explosion_soundEF_audioSource.clip = Skill_Explosion_soundEF;

            Skill_OnFire_audioSource.Stop();
            Skill_OnHeal_audioSource.Stop();

            Skill_Explosion_audioSource.Play();
            Skill_Explosion_soundEF_audioSource.Play();
        }
    }
}
