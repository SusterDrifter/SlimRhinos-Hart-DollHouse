using UnityEngine;

public class Needle : Interactable
{

    public override void Interact()
    {
        base.Interact();
        Chapter1_4.instance.needlePossessed = true;
        Chapter1_4.instance.EndingTrig();
        gameObject.SetActive(false);
    }
}
