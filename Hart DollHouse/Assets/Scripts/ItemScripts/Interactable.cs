using UnityEngine;

public class Interactable : MonoBehaviour {

    [SerializeField] private float interactRadius = 5f;

    bool isFocus = false;
    bool hasInteracted = false;
    bool highlighted = false;

    Transform playerPosition;

    [SerializeField] private Renderer rend;
    [SerializeField] private Shader originalShader;
    [SerializeField] private Shader highlightShader;

    public virtual void Interact()
    {
        // Being overriden 
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalShader = rend.material.shader;
        highlightShader = Shader.Find("graphs/Highlight");
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

    private void OnMouseOver()
    {
        if (!highlighted)
        {
            highlighted = true;
            HighLightActive();
        }
            
    }

    private void OnMouseExit()
    {
        if (highlighted)
        {
            highlighted = false;
            HighLightDeactive();
        }
    }

    private void HighLightActive()
    {
        rend.material.shader = highlightShader;
    }

    private void HighLightDeactive()
    {
        rend.material.shader = originalShader;
    }
}
