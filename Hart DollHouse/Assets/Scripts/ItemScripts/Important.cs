using UnityEngine;

/** 
 * Items that contributes to the story. 
 * It has viewing icon, description, and will
 * be picked up.
 */
public class Important : AudioableObject {

    [SerializeField] private Item item;
    private bool isUIActive = false;
    private bool mouseReleased = false;

    public override void Interact()
    {
        base.Interact();
        if (!isUIActive)
            ActivateUI();
    }

    new public void Update()
    {
        base.Update();
        if (isUIActive)
        {
            if (Input.GetMouseButtonUp(0))
                mouseReleased = true;

            if (Input.GetMouseButtonDown(1) || (mouseReleased && Input.GetMouseButtonDown(0)))
            {
                PickUp();
                DeactivateUI();
            }
        }
    }

    public void ActivateUI()
    {
        if (item.icon != null)
            MainUIManager.instance.GetItemUI().SetIcon(item.icon);
            
        if (item.description != null)
            MainUIManager.instance.GetItemUI().SetDesc(item.description);

        MainUIManager.instance.GetItemUI().ActivateUI();
        isUIActive = true;
    }

    public void DeactivateUI()
    {
        MainUIManager.instance.GetItemUI().SetIcon(null);
        MainUIManager.instance.GetItemUI().DeactivateUI();
        MainUIManager.instance.GetDialogueManager().NextHint();
        isUIActive = false;
    }

    public void PickUp() {
        bool outcome = Inventory.instance.AddToInvent(item);
        if (outcome) {
            MainUIManager.instance.GetNotificationUI().ShowNotification("You have obtained " 
                + item.itemName + ".");
            Destroy(gameObject);
        } 
    }
}
