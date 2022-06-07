using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr : MonoBehaviour
{
    public static MusicMgr Instance;
    public AudioSource musicSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayNewClip(AudioClip clip, bool loop, float volume)
    {
        if (musicSource.clip == clip)
        {
            return;
        }

        musicSource.clip = clip;
        musicSource.Play();
        musicSource.loop = loop;
        musicSource.volume = volume;
    }
}
