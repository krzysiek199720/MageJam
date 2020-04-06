using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public static AudioManager Instance { get { return instance; } }

    public Sound[] sounds;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;


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
