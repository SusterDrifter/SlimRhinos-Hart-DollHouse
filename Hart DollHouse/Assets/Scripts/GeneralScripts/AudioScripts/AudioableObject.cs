using UnityEngine;

public class AudioableObject : DialogueObject {

    public Sound viableSoundOn;
    public Sound viableSoundOff;
    public Sound nonviableSound;

    private bool isOn = false;

    private void InitialiseSound(Sound sound)
    {
        if (sound.clip != null && sound.source == null)
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
        // If not viable and has nonviable sound
        if (!base.isViable) { PlayClip(nonviableSound); }
        // If viable and off and has viable on sound
        else if (base.isViable && !isOn) { PlayClip(viableSoundOn); isOn = true; }
        // If viable and on and has viable off sound
        else if (base.isViable && isOn) { PlayClip(viableSoundOff); isOn = false; }
    }

    private void PlayClip(Sound sound)
    {
        InitialiseSound(sound);
        if (sound.clip != null)
            sound.source.Play();
    }
}
