using UnityEngine;

public class String : Interactable
{

    public override void Interact()
    {
        base.Interact();
        Chapter1_4.instance.stringPossessed = true;
        Chapter1_4.instance.EndingTrig();
        gameObject.SetActive(false);
    }
}
