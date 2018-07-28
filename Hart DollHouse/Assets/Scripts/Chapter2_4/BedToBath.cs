using UnityEngine;

public class BedToBath : Animatable {

    public override void Interact()
    {
        if (Inventory.instance.Contains(0))
            base.isViable = true;

        base.Interact();

        if (base.isViable)
            Chapter2_4.instance.BathroomScene();
    }
}
