using UnityEngine;

public class TrunkTrigger : MonoBehaviour {

    [SerializeField] private Dialogue trunkOpenDiag;

    private bool hasDiagInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasDiagInteract && trunkOpenDiag)
        {
            MainUIManager.instance.GetDialogueUIManager().GetManager().BeginDialogue(trunkOpenDiag);
            hasDiagInteract = true;
        }    
    }
}
