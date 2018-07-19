using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicAttackController : MonoBehaviour {

    private Animator animator;
    private Timer timer;
    [SerializeField] private BreathingSystemUIController breathingGame;
    private float winDuration = 11f;

    public static PanicAttackController instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        animator = GetComponent<Animator>();
        timer = GetComponent<Timer>();
    }

    public void PanicAttackAnim()
    {
        animator.SetTrigger("PanicAttack");  
    }

    public void StartRealBreathingGame()
    {
        if (breathingGame == null)
            breathingGame = MainUIManager.instance.GetBreathingManager();

        timer.SetDuration(winDuration);
        timer.RestartTimer();
        breathingGame.BeginBreathingSystem();
    }
}
