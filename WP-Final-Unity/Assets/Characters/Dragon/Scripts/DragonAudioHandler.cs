using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DragonAudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource subSource, hurtSource;
    [SerializeField] AudioClip sleep, walk, run;
    [SerializeField] AudioClip roar, hurt, death, basicAttack, clawAttack1, clawAttack2, clawAttack3, magicAttack1, magicAttack2, flameAttack;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Sleep()
    {
        if (source.isPlaying == false)
        {
            source.clip = sleep;
            source.Play();
        }
    }
    public void Walk()
    {
        if(source.isPlaying == false)
        {
            source.clip = walk;
            source.Play();
        }
    }
    public void Run()
    {
        if(source.isPlaying == false)
        {
            source.clip = run;
            source.Play();
        }
    }
    public void Roar()
    {
        source.clip = roar;
        source.PlayDelayed(0.7f);
    }
    public void Hurt()
    {
        hurtSource.clip = hurt;
        hurtSource.Play();
    }
    public void Death()
    {
        source.clip = death;
        source.Play();
    }
    public void BasicAttack()
    {
        source.clip = basicAttack;
        source.PlayDelayed(0.75f);
    }
    public void ClawAttack()
    {
        source.clip = clawAttack1;
        source.Play();
        Invoke(nameof(ClawAttack2), 1.1f);
    }
    void ClawAttack2()
    {
        subSource.clip = clawAttack2;
        subSource.Play();
        Invoke(nameof(ClawAttack3), 0.5f);
    }
    void ClawAttack3()
    {
        subSource.clip = clawAttack3;
        subSource.Play();
    }
    public void MagicAttack()
    {
        subSource.clip = magicAttack1;
        subSource.PlayDelayed(1f);
        Invoke(nameof(MagicAttack2), 2f);
    }
    void MagicAttack2()
    {
        subSource.clip = magicAttack2;
        subSource.Play();
    }
    public void FlameAttack()
    {
        source.clip = flameAttack;
        source.Play();
    }
}
