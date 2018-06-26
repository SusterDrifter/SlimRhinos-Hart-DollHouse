public class DialogueObject : Interactable {

    public Dialogue dialogue;

    public override void Interact()
    {
        base.Interact();
        if (dialogue != null)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(dialogue);
    }
}
