public class RhymeFour : ArticleObject
{
    private bool initDeactivate = false;

    private void LateUpdate()
    {
        if (gameObject.tag == "Interactable" && !initDeactivate)
        {
            this.enabled = false;
            initDeactivate = true;
        }
    }

    public override void Interact()
    {
        base.Interact();
        Chapter1_1.instance.rhymeGame.rhymeFour = true;
        Chapter1_1.instance.rhymeGame.ActivateFirstDoll();
    }
}