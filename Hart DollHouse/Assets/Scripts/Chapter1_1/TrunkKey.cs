public class TrunkKey : Important {

    private bool initDeactivate = false;

    private void LateUpdate()
    {
        base.Update();
        if (gameObject.tag == "Interactable" && !initDeactivate)
        {
            gameObject.SetActive(false);
            initDeactivate = true;
        }
    }

    public override void Interact()
    {
        base.Interact();
        Chapter1_1.instance.rhymeGame.Complete();
    }
}
