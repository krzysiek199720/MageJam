using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        instance = GetComponent<AudioManager>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }    
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (!IsSound || s == null)
            return;

        s.source.Play();
    }

    public static bool IsSound
    {
        get
        {
            return PlayerPrefs.GetInt("isSound", 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt("isSound", value ? 1 : 0);
        }
    }
}
