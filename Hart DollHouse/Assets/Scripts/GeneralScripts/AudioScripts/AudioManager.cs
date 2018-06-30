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
    [SerializeField] List<Sound> remoteSounds;
    [SerializeField] float fadeDuration = 0.5f;

    private Sound musicInPlay;
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

        GameObject[] remoteSource = GameObject.FindGameObjectsWithTag("RemoteSound");

        foreach (GameObject obj in remoteSource)
        {
            remoteSounds.Add(obj.GetComponent<AudioableObject>().sound);
        }

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

        foreach (Sound sound in remoteSounds)
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
                if (musicInPlay != null && musicInPlay.source.isPlaying)
                {
                    // FadeOut the music, FadeIn the new one
                    audioFader.FadeInNewSound(musicInPlay, sound, sound.source.volume, fadeDuration);
                }          
                // Keep track of this variable
                musicInPlay = sound;
            }
            sound.source.Play();
        }
    }

    public void PlayClip(Sound.SoundType type, string name)
    {
        Sound sound = GetSound(type, name);
        PlayClip(sound);
    }

    public Sound GetRemoteSound(string name)
    {
        Sound sound = null;
        sound = remoteSounds.Find(s => s.clipName == name);
        return sound;
    }

    public void PlayRemotely(Sound sound)
    {
        if (sound != null && sound.source != null && !sound.source.isPlaying)
            sound.source.Play();
    }

    public void PlayRemotely(string name)
    {
        Sound sound = GetRemoteSound(name);
        PlayRemotely(sound);
    }
}
