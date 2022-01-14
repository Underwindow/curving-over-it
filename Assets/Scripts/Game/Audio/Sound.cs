using UnityEngine;

[System.Serializable]
public class SoundEffect : Sound
{
    public SoundEffectType type;
}

[System.Serializable]
public class AudioSubtitle : Sound
{
    [Tooltip("If other subtitle is triggered should the current subtitle be interrupted?")]
    public bool interruptible;
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1;
    [Range(.1f, 3f)] public float pitch = 1;

    public bool loop = false, playOnAwake = false;

    [HideInInspector] public AudioSource source;

    public void InitByAudioSource(AudioSource audioSource, float userVolume = 1)
    {
        source = audioSource;
        source.clip = clip;
        source.volume = volume * userVolume;
        source.pitch = pitch;
        source.loop = loop;
        source.playOnAwake = playOnAwake;
    }

    public float GetTime() 
        => source.clip.length * (1 / source.pitch);
}
