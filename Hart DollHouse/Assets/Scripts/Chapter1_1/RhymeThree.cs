public class RhymeThree : Important
{

    public override void Interact()
    {
        base.Interact();
        Chapter1_1.instance.rhymeGame.rhymeThree = true;
        Chapter1_1.instance.rhymeGame.Complete();
    }
}
