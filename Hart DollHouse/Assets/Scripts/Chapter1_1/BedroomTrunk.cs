using UnityEngine;

public class BedroomTrunk : Animatable {

    public override void Interact()
    {
        if (Inventory.instance.Contains(2))
            base.isViable = true;

        base.Interact();

        if (base.isViable)
        {
            Chapter1_1.instance.hasTrunkOpened = true;
            Chapter1_1.instance.EndSequence();
        }        
    }
}
