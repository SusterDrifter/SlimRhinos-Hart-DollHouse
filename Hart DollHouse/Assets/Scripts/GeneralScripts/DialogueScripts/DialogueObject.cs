public class DialogueObject : Interactable {

    public Dialogue viableDialogue;
    public Dialogue nonviableDialogue;

    public override void Interact()
    {
        base.Interact();
        if (viableDialogue && base.isViable)
        {
            TriggerDialogue(viableDialogue);
        } else if (nonviableDialogue && !base.isViable)
        {
            TriggerDialogue(nonviableDialogue);
        }
    }

    public void TriggerDialogue(Dialogue dialogue) {
        MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(dialogue);
    }
}
