using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup mixer;

    public bool play;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;

    public bool loop;
    public bool introLoop;
    public float loopStart;
    public float loopEnd;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public float time;

}
