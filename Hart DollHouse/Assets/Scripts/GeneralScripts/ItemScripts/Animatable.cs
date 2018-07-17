using UnityEngine;

public class Animatable : AudioableObject {

    [SerializeField] private string triggerName = "Interact";

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
        animator.SetBool(triggerName, base.state);
        if (base.switchable)
            base.state = !base.state;
    }
}
