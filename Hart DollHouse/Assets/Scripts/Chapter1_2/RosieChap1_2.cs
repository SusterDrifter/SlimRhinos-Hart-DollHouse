using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class RosieChap1_2 : DialogueObject {

    [SerializeField] private GameObject rosieOnBed;
    [SerializeField] private GameObject wardrobeKey;
    [SerializeField] public Sound dragSound;

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(DestroyObjTransition(0.1f, 5f));
    }

    IEnumerator DestroyObjTransition(float fadeDuration, float delayDur)
    {
        MainUIManager.instance.GetBlackScreen().BlackFadeIn(fadeDuration);

        if (dragSound.clip)
        {
            dragSound.source = gameObject.AddComponent<AudioSource>();
            dragSound.source.clip = dragSound.clip;

            dragSound.source.volume = dragSound.volume;
            dragSound.source.pitch = dragSound.pitch;
            dragSound.source.spatialBlend = dragSound.spatialBlend;

            dragSound.source.loop = dragSound.loop;
            //dragSound.source.playOnAwake = dragSound.playOnAwake;
            dragSound.source.playOnAwake = true;
        }

        yield return new WaitForSecondsRealtime(fadeDuration);

        //if (dragSound.clip)
          //  dragSound.source.Play();

        if (rosieOnBed && wardrobeKey)
        {
            rosieOnBed.gameObject.SetActive(true);
            wardrobeKey.gameObject.SetActive(true);
        }

        MainUIManager.instance.GetBlackScreen().BlackFadeOutDelayBy(delayDur);
        gameObject.SetActive(false);
    }
}
