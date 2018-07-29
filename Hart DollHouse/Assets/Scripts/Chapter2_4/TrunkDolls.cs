using UnityEngine;

public class TrunkDolls : DialogueObject {

    [SerializeField] private Sound breakingSound;

    public override void Interact()
    {
        base.Interact();
        if (breakingSound.clip)
            Chapter2_4.instance.DeactivateTransition(this.gameObject, breakingSound);
    }
}
