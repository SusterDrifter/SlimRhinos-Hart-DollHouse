using UnityEngine;
using System.Collections;

public class RosieChap1_2 : DialogueObject {

    [SerializeField] private GameObject rosieOnBed;
    [SerializeField] private GameObject wardrobeKey;

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(DestroyObjTransition(0.1f, 5f));
    }

    IEnumerator DestroyObjTransition(float fadeDuration, float delayDur)
    {
        MainUIManager.instance.GetBlackScreen().BlackFadeIn(fadeDuration);
        yield return new WaitForSecondsRealtime(fadeDuration);
        if (rosieOnBed)
        {
            rosieOnBed.gameObject.SetActive(true);
            wardrobeKey.gameObject.SetActive(true);
        }
        MainUIManager.instance.GetBlackScreen().BlackFadeOutDelayBy(delayDur);
        gameObject.SetActive(false);
    }
}
