using UnityEngine.Audio;
using System;
using UnityEngine;

public class MyAudioManager : MonoBehaviour //used to control audio files by implementing the Sounds class
{
    public Sounds[] sounds;

    void Awake()
    {
        foreach (Sounds s in sounds) //gets each component from Sounds for each audio file using the manager
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
                Play(s.name);
            }
        }
    }

    void Update()
    {
        foreach (Sounds s in sounds)
        {
            if (s.introLoop == true) //used to loop a song from a point that is not the very beginning of the audio file
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
