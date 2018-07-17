using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LightFlicker : MonoBehaviour {

    [SerializeField] private Sound[] flickerSounds;

    [SerializeField] private Light lightFlick;
    [SerializeField] private float defaultIntensity = 0f;

    [SerializeField] private float minIntensity = 20f;
    [SerializeField] private float maxIntensity;

    [SerializeField] private float averageDuration = 0.75f;
    [SerializeField] private float variationDuration = 0.3f;

    [SerializeField] private float averageInterval = 8f;
    [SerializeField] private float variationInterval = 2f;

    private bool flicker = false;
    private float range;
    private Timer timer;

	void Start () {
        lightFlick = GetComponent<Light>();
        maxIntensity = lightFlick.intensity;
        range = maxIntensity - minIntensity;

        foreach (Sound sound in flickerSounds)
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

        lightFlick.intensity = defaultIntensity;
        timer = GetComponent<Timer>();
        SetFlickerEffect(true);
    }
	
	void Update () {
        if (timer.HasRunOut())
            flicker = true;
        if (flicker)
            StartCoroutine(FlickerEffect(averageDuration - (Random.value * variationDuration)));
	}

    public void SetFlickerEffect(bool state)
    {
        if (state)
        {
            timer.SetDuration(averageInterval - (Random.value * variationInterval));
            timer.StartTimer();
        } else
        {
            timer.ResetTimer();
            flicker = false;
        } 
    }

    IEnumerator FlickerEffect(float duration)
    {
        SetFlickerEffect(false);
        float durationLeft = duration;

        int soundIndex = Mathf.FloorToInt(Random.value * (flickerSounds.Length));

        if (flickerSounds[soundIndex].clip != null && flickerSounds[soundIndex].source != null)
            flickerSounds[soundIndex].source.Play();

        while (durationLeft > 0)
        {
            float random = Random.value * range;
            lightFlick.intensity = maxIntensity - random;
            durationLeft -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SetFlickerEffect(true);
        lightFlick.intensity = defaultIntensity;
    }
}
