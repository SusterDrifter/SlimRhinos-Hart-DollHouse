using UnityEngine;

public class Scissors : Interactable {

    public override void Interact()
    {
        base.Interact();
        Chapter1_4.instance.scissorsPossessed = true;
        Chapter1_4.instance.EndingTrig();
        gameObject.SetActive(false);
    }
}
