using UnityEngine;

public class TrunkDolls : DialogueObject {

    public override void Interact()
    {
        base.Interact();
        Chapter2_4.instance.DeactivateTransition(this.gameObject);
    }
}
