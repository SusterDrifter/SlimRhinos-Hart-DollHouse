using System.Collections;
using UnityEngine;

public class AudioFader : MonoBehaviour {

    public void FadeIn(Sound sound, float duration = 0.5f, float finalVolume = 1f)
    {
        StartCoroutine(FadeSound(sound, 0, finalVolume, duration));
    }

    public void FadeOut(Sound sound, float duration = 0.5f, float finalVolume = 0f)
    {
        StartCoroutine(FadeSound(sound, sound.source.volume, finalVolume, duration));
    }

    public void FadeInNewSound(Sound oldSound, Sound newSound, float endVol, float fadeDuration = 0.5f)
    {
        StartCoroutine(FadeNewSound(oldSound, newSound, endVol, fadeDuration));
    }

    public IEnumerator FadeSound(Sound sound, float startVol, float endVol, float duration) {

        float startTime = Time.time;
        float timeSinceStarted = Time.time - startTime;
        float percentageComplete = timeSinceStarted / duration;

        sound.source.volume = startVol;
        AudioManager.instance.PlayClip(sound);

        while (percentageComplete < 1) {
            timeSinceStarted = Time.time - startTime;
            percentageComplete = timeSinceStarted / duration;

            float curVol = Mathf.Lerp(startVol, endVol, percentageComplete);
            sound.source.volume = curVol;
            yield return null;
        }

        if (endVol == 0f)
            sound.source.Stop();
    }

    private IEnumerator FadeNewSound(Sound oldSound, Sound newSound, float endVol, float fadeDuration)
    {
        yield return StartCoroutine(FadeSound(oldSound, oldSound.source.volume, 0, fadeDuration));
        oldSound.source.Stop();
        StartCoroutine(FadeSound(newSound, newSound.source.volume, endVol, fadeDuration));
    }
}
