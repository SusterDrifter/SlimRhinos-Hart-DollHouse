public class RhymeOne : Important {

    public override void Interact()
    {
        base.Interact();
        Chapter1_1.instance.rhymeGame.rhymeOne = true;
        Chapter1_1.instance.rhymeGame.Complete();
    }
}
