using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingSystemUIController : MonoBehaviour {

    [SerializeField] BreathingCircBarUIHandle handle;
    [SerializeField] UIFader fader;
    [SerializeField] CanvasGroup UIElement;

    public bool begin = false;
    private bool hasStarted = false;

	// Use this for initialization
	void Start () {
        fader = GetComponent<UIFader>();
        UIElement = GetComponent<CanvasGroup>();
        handle = GetComponentInChildren<BreathingCircBarUIHandle>();
        UIElement.alpha = 0;
    }

    private void Update()
    {
        if(begin && !hasStarted)
        {
            BeginBreathingSystem();
            hasStarted = true;
        }
    }
    public void BeginBreathingSystem()
    {
        fader.FadeIn(UIElement, 0.5f);
        handle.StartBreathingSystem();
    }
}
