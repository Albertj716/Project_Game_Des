using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds //aspects of any audio that would need to be controlled
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup mixer;

    public bool play;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;

    public bool loop; //loops back to very beginning
    public bool introLoop; //used to loop back to a specific time in the song from a specific end time
    public float loopStart;
    public float loopEnd;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public float time;

}
