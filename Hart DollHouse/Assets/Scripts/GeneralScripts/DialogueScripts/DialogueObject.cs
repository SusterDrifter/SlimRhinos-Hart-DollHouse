using UnityEngine;

public class DialogueObject : Interactable {

    [SerializeField] private Dialogue oldViableDiag;
    [SerializeField] private Dialogue oldNonviableDiag;
    [SerializeField] private Dialogue newViableDiag;
    [SerializeField] private Dialogue newNonviableDiag;
    
    public bool useNewDiag = false;

    public override void Interact()
    {
        base.Interact();
        if (base.isViable && !useNewDiag && oldViableDiag)
            TriggerDialogue(oldViableDiag);
        else if (!base.isViable && !useNewDiag && oldNonviableDiag)
            TriggerDialogue(oldNonviableDiag);
        else if (base.isViable && useNewDiag && newViableDiag)
            TriggerDialogue(newViableDiag);
        else if (!base.isViable && useNewDiag && newNonviableDiag)
            TriggerDialogue(newNonviableDiag);
    }

    public virtual void TriggerDialogue(Dialogue dialogue) {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(dialogue);
    }
}
