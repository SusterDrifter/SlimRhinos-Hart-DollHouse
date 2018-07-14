using UnityEngine;

public class AudioableObject : DialogueObject {

    public Sound viableSound;
    public Sound nonviableSound;
    private AudioSource source;

    private void Awake()
    {
        if (viableSound.clip != null || nonviableSound.clip != null)
            source = gameObject.AddComponent<AudioSource>();

        if (viableSound.clip != null)
        {
            viableSound.source = source;
            viableSound.source.clip = viableSound.clip;

            viableSound.source.volume = viableSound.volume;
            viableSound.source.pitch = viableSound.pitch;
            viableSound.source.spatialBlend = viableSound.spatialBlend;

            viableSound.source.loop = viableSound.loop;
            viableSound.source.playOnAwake = viableSound.playOnAwake;
        }

        if (nonviableSound.clip != null)
        {
            nonviableSound.source = source;
            nonviableSound.source.clip = nonviableSound.clip;

            nonviableSound.source.volume = nonviableSound.volume;
            nonviableSound.source.pitch = nonviableSound.pitch;
            nonviableSound.source.spatialBlend = nonviableSound.spatialBlend;

            nonviableSound.source.loop = nonviableSound.loop;
            nonviableSound.source.playOnAwake = nonviableSound.playOnAwake;
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (viableSound.clip != null && base.isViable)
        {
            PlayClip(viableSound);
        } else if (nonviableSound.clip != null && !base.isViable)
        {
            PlayClip(nonviableSound);
        }
    }

    private void PlayClip(Sound sound)
    {
        sound.source.Play();
    }
}
