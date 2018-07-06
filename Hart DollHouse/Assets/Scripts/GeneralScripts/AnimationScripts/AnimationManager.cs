using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

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
    }
    #endregion
}
