using System;
using UnityEngine;

public class SubtitleEnterEventArgs : EventArgs
{
    public AudioSubtitle audioSub;
    public Collider2D other;

    public SubtitleEnterEventArgs(AudioSubtitle audioSub, Collider2D other)
    {
        this.audioSub = audioSub;
        this.other = other;
    }
}