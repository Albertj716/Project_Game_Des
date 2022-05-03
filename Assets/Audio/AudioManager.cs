using UnityEngine.Audio;
using System;
using UnityEngine;

public class MyAudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    void Awake()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.time = s.time;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    void Start()
    {
        foreach (Sounds s in sounds)
        {
            if (s.play == true)
            {
                Debug.Log(s.name);
                Play(s.name);
            }
        }
    }

    void Update()
    {
        foreach (Sounds s in sounds)
        {
            if (s.introLoop == true)
            {
                if (s.source.time >= s.loopEnd)
                {
                    s.source.time = s.loopStart;
                }
            }
        }
    }

    public void Play (string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
