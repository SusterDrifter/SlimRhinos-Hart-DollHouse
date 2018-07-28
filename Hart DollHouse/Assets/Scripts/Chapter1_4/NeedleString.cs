using UnityEngine;

public class NeedleString : Interactable
{

    public override void Interact()
    {
        base.Interact();
        Chapter1_4.instance.needleStringPossessed = true;
        Chapter1_4.instance.EndingTrig();
        gameObject.SetActive(false);
    }
}
