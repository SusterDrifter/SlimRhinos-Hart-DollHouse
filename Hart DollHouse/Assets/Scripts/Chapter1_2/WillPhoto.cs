using UnityEngine;

public class WillPhoto : ArticleObject
{

    public override void Interact()
    {
        base.Interact();
        Chapter1_2.instance.willPhoto = true;
        Chapter1_2.instance.EndingSeq();
    }
}
