using UnityEngine;

public class Animatable : DialogueObject {

    [SerializeField] private Anim anim;

    private void Awake()
    {
        if (anim != null)
        {
            anim.animator = gameObject.AddComponent<Animator>();
            anim.animator.runtimeAnimatorController = anim.controller;
            anim.animator.applyRootMotion = anim.applyRootMotion;
            anim.animator.speed = anim.speed;
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (anim != null)
        {
            PlayAnim();
        }
    }

    public void PlayAnim()
    {
        if (anim != null)
            anim.animator.Play(anim.animationName);
    }

    public Anim GetAnim()
    {
        return anim;
    }
}
