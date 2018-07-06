public class RhymeTwo : Important
{

    public override void Interact()
    {
        base.Interact();
        Chapter1_1.instance.rhymeGame.rhymeTwo = true;
        Chapter1_1.instance.rhymeGame.Complete();
    }
}
