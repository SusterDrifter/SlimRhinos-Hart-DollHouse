using UnityEngine;

public class EmilPhoto : ArticleObject {

    public override void Interact()
    {
        base.Interact();
        Chapter1_2.instance.emilPhoto = true;
        Chapter1_2.instance.EndingSeq();
    }
}
