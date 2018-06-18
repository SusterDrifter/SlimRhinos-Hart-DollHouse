using UnityEngine;

public class ArticleObject : Interactable {

    [SerializeField] private Sprite icon;
    [SerializeField] private Dialogue desc;

    private bool isUIActive = false;
    private bool mouseReleased = false;
    private bool isDescActive = false;

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isDescActive)
                    Examine();
                else
                    FinishExamine();
            }
                
            if (Input.GetMouseButtonDown(1) || (mouseReleased && Input.GetMouseButtonDown(0)))
                DeactivateUI();
        }
    }

    private void ActivateUI()
    {
        if (icon != null)
            MainUIManager.instance.GetArticleManager().SetIcon(icon);
        if (desc != null)
            MainUIManager.instance.GetArticleManager().SetDesc(desc);

        MainUIManager.instance.GetArticleManager().ActivateUI();
        isUIActive = true;
    }

    private void DeactivateUI()
    {
        FinishExamine();
        MainUIManager.instance.GetArticleManager().SetIcon(null);
        MainUIManager.instance.GetArticleManager().DeactivateUI();
        isUIActive = false;
    }

    private void Examine()
    {
        MainUIManager.instance.GetArticleManager().Examine();
        isDescActive = true;
    }

    private void FinishExamine()
    {
        MainUIManager.instance.GetArticleManager().FinishExamine();
        isDescActive = false;
    }
}
