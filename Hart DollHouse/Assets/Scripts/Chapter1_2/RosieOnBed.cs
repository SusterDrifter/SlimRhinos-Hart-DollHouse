using UnityEngine;
using System.Collections;

public class RosieOnBed : DialogueObject {

    private bool hasDeactivate = false;

    new void Update()
    {
        base.Update();


        if (gameObject.tag == "Interactable" && !hasDeactivate)
        {
            gameObject.SetActive(false);
            hasDeactivate = true;
        }

    }

    public override void Interact()
    {
        base.Interact();
        StartCoroutine(DestroyObjTransition(0.1f, 3f));
    }

    IEnumerator DestroyObjTransition(float fadeDuration, float delayDur)
    {
        MainUIManager.instance.GetBlackScreen().BlackFadeIn(fadeDuration);
        MainUIManager.instance.GetBlackScreen().BlackFadeOutDelayBy(delayDur);
        yield return new WaitForSecondsRealtime(fadeDuration);
        gameObject.SetActive(false);
    }
}
