using UnityEngine;

public class Animatable : AudioableObject {

    [SerializeField] private string triggerName = "Interact";
    private bool state = false;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        
        base.Interact();
        if (base.isViable)
            PlayAnim();
    }

    public void PlayAnim()
    {
        state = !state;
        animator.SetBool(triggerName, state);
    }
}
