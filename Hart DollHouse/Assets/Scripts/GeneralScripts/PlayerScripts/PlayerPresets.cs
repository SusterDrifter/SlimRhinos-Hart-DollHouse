using UnityEngine;

public class PlayerPresets : MonoBehaviour {

    public static PlayerPresets instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("WARNING! More than one instance of PlayerPresets is created.");
            return;
        }
        instance = this;
    }
    [SerializeField] private Preset currPreset;

    public Preset GetPreset() {
        return currPreset;
    }
}
