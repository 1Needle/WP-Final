using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] walks, runs;
    int walkCount, walkIdx, runCount, runIdx;
    [SerializeField] AudioClip roar, attack, damaged, death;
    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        walkCount = walks.Length;
        walkIdx = 0;
        runCount = runs.Length;
        runIdx = 0;
    }

    public void Walk()
    {
        if (source.isPlaying == false)
        {
            source.clip = walks[walkIdx];
            source.Play();
            if (++walkIdx >= walkCount)
                walkIdx = 0;
        }
    }
    public void Run()
    {
        if (source.isPlaying == false)
        {
            source.clip = runs[runIdx];
            source.Play();
            if (++runIdx >= runCount)
                runIdx = 0;
        }
    }
    public void Roar()
    {
        source.clip = roar;
        source.Play();
    }
    public void Attack()
    {
        source.clip = attack;
        source.PlayDelayed(0.9f);
    }
    public void Damaged()
    {
        source.clip = damaged;
        source.Play();
    }
    public void Death()
    {
        source.clip = death;
        source.Play();
    }
}
