using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBoxKey : Important {

    private bool hasDeactivate = false;
	
	new void Update () {
        base.Update();
        if (gameObject.tag == "Interactable" && !hasDeactivate)
        {
            gameObject.SetActive(false);
            hasDeactivate = true;
        }
	}
}
