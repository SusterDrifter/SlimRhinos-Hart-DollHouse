using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {

    public enum SoundType { SoundEffect, BackgroundMusic };

    public string clipName = "Default Name";
    public SoundType type = SoundType.SoundEffect;

    public AudioClip clip;
    [HideInInspector] public AudioSource source;

    public bool loop = false;
    public bool playOnAwake = false;

    [Range(0f, 1f)] public float volume = 1f;
    [Range(0f, 3f)] public float pitch = 1f;
    [Range(0f, 1f)] public float spatialBlend = 0.8f;
}