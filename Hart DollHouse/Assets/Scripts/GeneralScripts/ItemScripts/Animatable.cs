using UnityEngine;

public class Animatable : DialogueObject {

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
        if (Condition())
            PlayAnim();
        else
            Consequence();
    }

    public void PlayAnim()
    {
        state = !state;
        animator.SetBool(triggerName, state);
    }

    public virtual bool Condition()
    {
        return true;
    }

    public virtual void Consequence() { }
}
