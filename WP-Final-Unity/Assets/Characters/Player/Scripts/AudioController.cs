using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip Hit;
    [SerializeField] AudioClip GetHit;
    [SerializeField] AudioClip Defending_GetHit;

    private string str_audioclip = "null";

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Receive_Audio_Hit()
    {
        audioSource.volume = 0.5f;
        str_audioclip = "audio_Hit";
        audioSource.clip = Hit;
        audioSource.Play();
    }

    public void Receive_Audio_GetHit()
    {
        audioSource.volume = 1f;
        str_audioclip = "audio_GetHit";
        audioSource.clip = GetHit;
        audioSource.Play();
    }

    public void Receive_Audio_Defending_GetHit()
    {
        audioSource.volume = 0.5f;
        str_audioclip = "audio_Defending_GetHit";
        audioSource.clip = Defending_GetHit;
        audioSource.Play();
    }
}
