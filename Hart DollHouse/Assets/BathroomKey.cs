using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomKey : Important {

	public override void Interact()
    {
        base.Interact();
        Chapter2_4.instance.DoorNewDiag();
    }
}
