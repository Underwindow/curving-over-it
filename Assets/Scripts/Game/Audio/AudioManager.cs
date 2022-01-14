using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public SoundEffect[] sounds;
    public float defaultUserVolume;

    [HideInInspector] public List<SoundEffect> playingSounds;
    [HideInInspector] public float userVolume;
    
    private void Awake()
    {
        userVolume = PlayerPrefs.GetFloat("Volume", defaultUserVolume);
        foreach (SoundEffect sound in sounds)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            sound.InitByAudioSource(audioSource);
        }
    }

    public SoundEffect Play (SoundEffectType soundType, uint delaySec = 0)
    {
        var s = GetSound(soundType);
        s.source.Play(delaySec * 44100);
        if (!playingSounds.Contains(s))
            playingSounds.Add(s);

        return s;
    }

    public SoundEffect GetSound(SoundEffectType soundType)
    {
        return Array.Find(sounds, sound => sound.type == soundType);
    }
}
