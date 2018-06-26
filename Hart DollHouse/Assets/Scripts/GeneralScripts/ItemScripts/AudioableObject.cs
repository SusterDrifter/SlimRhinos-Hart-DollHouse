using UnityEngine;

public class AudioableObject : DialogueObject {

    public Sound sound;

    private void Start()
    {
        if (sound != null)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;

            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
        }

    }

    public override void Interact()
    {
        base.Interact();
        if (sound != null) {
            PlayClip();
        }
    }

    private void PlayClip()
    {
        sound.source.Play();
    }
}
