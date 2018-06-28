using UnityEngine;

[System.Serializable]
public class Anim {

    public string objName;
    public string animationName = "Interact";
    public Animator animator;
    public RuntimeAnimatorController controller;
    public bool applyRootMotion = false;
    [Range(0, 5f)] public float speed = 1f;
}
