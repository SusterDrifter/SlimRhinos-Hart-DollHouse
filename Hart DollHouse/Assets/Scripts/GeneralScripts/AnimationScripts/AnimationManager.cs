using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    [SerializeField] private List<Anim> remoteAnim;

    #region Singleton
    public static AnimationManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("WARNING! More than one instance of AnimationManager is created.");
            return;
        }
        instance = this;

        GameObject[] remote = GameObject.FindGameObjectsWithTag("RemoteAnim");
        foreach(GameObject obj in remote)
        {
            remoteAnim.Add(obj.GetComponent<Animatable>().GetAnim());
        }
    }
    #endregion

    public Anim GetAnim(string name)
    {
        return remoteAnim.Find(anim => anim.objName == name);
    }

    public void PlayAnimOnObj(string name)
    {
        Anim anim = GetAnim(name);
        if (anim != null)
            anim.animator.Play(anim.animationName);
    }
}
