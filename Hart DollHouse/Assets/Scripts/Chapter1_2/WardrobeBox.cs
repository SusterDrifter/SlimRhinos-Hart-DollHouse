using UnityEngine;

public class WardrobeBox : Animatable {

    public override void Interact()
    {
        if (Inventory.instance.Contains(7))
            base.isViable = true;

        base.Interact();
    }
}
