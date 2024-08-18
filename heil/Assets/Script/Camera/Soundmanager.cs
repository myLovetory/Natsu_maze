using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Soundmanager : MonoBehaviour
{
    private AudioSource source;
    public static Soundmanager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();

    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    
}


