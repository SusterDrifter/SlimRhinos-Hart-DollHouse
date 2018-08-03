using UnityEngine;
using System.Collections;

public class RosieOnBed : DialogueObject {

    private bool hasDeactivate = false;
    [SerializeField] private GameObject bedToCor;
    [SerializeField] private GameObject bedToBath;

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

        if (bedToBath)
            bedToBath.GetComponent<Animatable>().useNewDiag = true;
        if (bedToCor)
            bedToCor.GetComponent<Animatable>().useNewDiag = true;
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
