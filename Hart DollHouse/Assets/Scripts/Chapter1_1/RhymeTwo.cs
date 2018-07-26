using System.Collections;
using UnityEngine;

public class RhymeTwo : ArticleObject
{
    [SerializeField] private GameObject wardrobeBox;
    private bool hasInteracted = false;

    public override void Interact()
    {
        hasInteracted = true;
        base.Interact();
        if (wardrobeBox)
            wardrobeBox.GetComponent<WardrobeBox>().useNewDiag = true;
        Chapter1_1.instance.rhymeGame.rhymeTwo = true;
        Chapter1_1.instance.rhymeGame.ActivateOtherDoll();
     
    }

    new private void Update()
    {
        base.Update();
        if (hasInteracted)
        {
            if (!MainUIManager.instance.isUIActive)
            {
                StartCoroutine(DestroyObjTransition(0.5f, 2f));
                hasInteracted = false;
            }
        }
    }

    IEnumerator DestroyObjTransition(float fadeDuration, float delayDur)
    {
        MainUIManager.instance.GetBlackScreen().BlackFadeIn(fadeDuration);
        yield return new WaitForSecondsRealtime(fadeDuration);
        MainUIManager.instance.GetBlackScreen().BlackFadeOutDelayBy(delayDur);
        gameObject.SetActive(false);
    }
}
