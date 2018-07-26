using UnityEngine;

public class Interactable : MonoBehaviour {

    public float interactRadius = 5f;

    public bool isFocus = false;
    bool hasInteracted = false;
    public bool isViable = true;

    public bool state = true;
    public bool switchable = false;

    Transform playerPosition;
    private void Start()
    {
        gameObject.tag = "Interactable";
    }
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
