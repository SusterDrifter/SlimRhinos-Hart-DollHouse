public class RhymeTwo : ArticleObject
{
    public override void Interact()
    {
        base.Interact();
        Chapter1_1.instance.rhymeGame.rhymeTwo = true;
        Chapter1_1.instance.rhymeGame.ActivateOtherDoll();
    }
}
