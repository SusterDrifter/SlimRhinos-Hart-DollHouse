using UnityEngine;

public class BedToCorTrig : Animatable {

    [SerializeField] public GameObject[] triggered;
    [SerializeField] public GameObject wardrobeLeft;
    [SerializeField] public GameObject wardrobeRight;
    [SerializeField] public GameObject trunkCover;

    public override void Interact()
    {
        base.Interact();
        foreach (GameObject obj in triggered)
            obj.GetComponent<DialogueObject>().useNewDiag = true;

        if (wardrobeLeft && wardrobeRight && trunkCover)
        {
            wardrobeLeft.GetComponent<Animatable>().isViable = true;
            wardrobeRight.GetComponent<Animatable>().isViable = true;
            trunkCover.GetComponent<Animatable>().isViable = true;
        }
    }
}
