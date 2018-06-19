using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField] private float interactRadius = 5f;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform playerPosition;

    public virtual void Interact()
    {
        // Being overriden 
    }

    public void Update() {
        if (isFocus && !hasInteracted) {
            float distance = Vector3.Distance(playerPosition.position, transform.position);
            if (distance <= interactRadius) {
                hasInteracted = true;
                Interact();
            }
        }    
    }

    public void OnFocused(Transform playerPosition) {
        isFocus = true;
        this.playerPosition = playerPosition;
        hasInteracted = false;
    }

    public void OnDefocused() {
        isFocus = false;
        playerPosition = null;
        hasInteracted = false;
    }
}
