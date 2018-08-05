using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Handles sound clips that are meant to be played from
 * the AudioManager and not tied to any particular gameobjects
 * e.g UI soundEffects, PlayerDeath, Music, etc, or to be 
 * triggered remotely.
 */
 [RequireComponent(typeof(AudioFader))]
public class AudioManager : MonoBehaviour {

    [SerializeField] Sound[] soundEffects;
    [SerializeField] Sound[] backgroundMusic;

    [SerializeField] public Sound musicInPlay;
    public AudioFader audioFader;

    #region Singleton
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) 
           instance = this;
        else
           Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        #region Initialisation
        foreach (Sound sound in soundEffects)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;

            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }

        foreach (Sound sound in backgroundMusic)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;

            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }

        audioFader = GetComponent<AudioFader>();
        #endregion
    }
    #endregion

    public Sound GetSound(Sound.SoundType type, string name)
    {
        Sound sound = null;

        if (type == Sound.SoundType.SoundEffect)
            sound = Array.Find(soundEffects, s => s.clipName == name);

        if (type == Sound.SoundType.BackgroundMusic)
            sound = Array.Find(backgroundMusic, s => s.clipName == name);
        return sound;
    }

    /**
     * Responsible for searching and playing the sound clip
     * that is played.
     */
    public void PlayClip(Sound sound)
    {
        if (sound != null && sound.source != null && !sound.source.isPlaying)
        {
            if (sound.type == Sound.SoundType.BackgroundMusic)
            {
                // Check if there is a current music playing
                if (musicInPlay.clip != null && musicInPlay.source.isPlaying)
                {
                    // Stop old music
                    musicInPlay.source.Stop();
                }

                // Keep track of this variable
                musicInPlay.clipName = sound.clipName;
                musicInPlay.clip = sound.clip;
                musicInPlay.source = sound.source;

                musicInPlay.volume = sound.source.volume;
                musicInPlay.loop = sound.source.loop;
                musicInPlay.pitch = sound.source.pitch;
                musicInPlay.spatialBlend = sound.source.spatialBlend;
                musicInPlay.playOnAwake = sound.playOnAwake;

                musicInPlay.source.volume = sound.source.volume;
                musicInPlay.source.loop = sound.source.loop;
                musicInPlay.source.pitch = sound.source.pitch;
                musicInPlay.source.spatialBlend = sound.source.spatialBlend;
                musicInPlay.source.playOnAwake = sound.playOnAwake;

            }
            sound.source.Play();
            
        }
    }

    public void PlayClip(Sound.SoundType type, string name)
    {
        Sound sound = GetSound(type, name);
        PlayClip(sound);
    }

    public void PlayRemotely(Sound sound)
    {
        if (sound != null && sound.source != null && !sound.source.isPlaying)
            sound.source.Play();
    }

    public void FadeOutMusic()
    {
        if (musicInPlay.clip && musicInPlay.source)
            audioFader.FadeOut(musicInPlay);
    }
}
