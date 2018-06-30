using UnityEngine;

[RequireComponent(typeof(Timer))]
/**
 * Handles the rotation of the bar. 
 * Also triggers of the animation, sound effect, effect,
 * and passing out.
 */
public class BreathingCircBarUIHandle : CircularBarUIController {

    [SerializeField] Timer timer;
    [SerializeField] float timerDuration = 10f;
    [SerializeField] float increaseTimer = 0.75f;
    [SerializeField] float decreaseTimer = 0.75f;

    [SerializeField] float ezTimerDuration = 10f;
    [SerializeField] float ezIncreaseTimer = -0.2f;
    [SerializeField] float ezDecreaseTimer = 0.5f;

    private float oriTimerDuration;
    private float oriIncreaseTimer;
    private float oriDecreaseTimer;

    private bool hasPassedOut = false;
    private bool start = false;
    private bool tutorialMode = false;
    public bool hasFinished = false;

    private Sound heartbeatSfx;

	// Use this for initialization
	void Start () {
        timer = GetComponent<Timer>();
        base.SetButton(GetComponent<RectTransform>());

        oriTimerDuration = timerDuration;
        oriDecreaseTimer = decreaseTimer;
        oriIncreaseTimer = increaseTimer;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (start)
        {
            base.UpdateCircBarUI();
        }
    }

    private void Update()
    {
        if (start)
        {
            CheckInput();
        }
    }


    private void CheckInput() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sliderValue <= maxCorrectVal && sliderValue >= minCorrectVal)
            {
                timer.IncBy(increaseTimer);
                heartbeatSfx.source.volume -= (0.10f * Time.deltaTime);
            } else {
                timer.DecBy(decreaseTimer);
                heartbeatSfx.source.volume += (0.05f * Time.deltaTime);
            }
            CameraPPSControl.instance.Flash();
        }

        if (!hasPassedOut)
        {
            PassOutEffect();
            heartbeatSfx.source.volume += (0.05f * Time.deltaTime);

            if (timer.HasRunOut())
            {
                if(!tutorialMode)
                    hasPassedOut = true;

                heartbeatSfx.source.Stop();
                start = false;
                hasFinished = true;
            }
        }
    }

    public void StartBreathingSystem()
    {
        hasFinished = false;

        timer.SetDuration(timerDuration);
        timer.RestartTimer();

        // Begin heartbeat sfx
        heartbeatSfx = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "Heartbeat");
        heartbeatSfx.source.volume = 0.5f;
        AudioManager.instance.PlayClip(heartbeatSfx);

        start = true;
    }

    private void PassOutEffect()
    {
        float percentage = timer.PercentageFromZero();
        CameraPPSControl.instance.PassingOutEffect(percentage);
    }

    public void BeginnerMode()
    {
        timerDuration = ezTimerDuration;
        increaseTimer = ezIncreaseTimer;
        decreaseTimer = ezDecreaseTimer;

        tutorialMode = true;
    }

    public void HardcoreMode()
    {
        timerDuration = oriTimerDuration;
        increaseTimer = oriIncreaseTimer;
        decreaseTimer = oriDecreaseTimer;

        tutorialMode = false;
    }
}
