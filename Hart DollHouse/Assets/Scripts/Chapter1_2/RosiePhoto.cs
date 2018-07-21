using UnityEngine;

public class RosiePhoto : ArticleObject {

    public override void Interact()
    {
        base.Interact();
        Chapter1_2.instance.rosiePhoto = true;
        Chapter1_2.instance.EndingSeq();
    }
}
