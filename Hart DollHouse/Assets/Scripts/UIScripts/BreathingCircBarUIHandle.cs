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

    private bool hasPassedOut = false;
    private bool start = false;
    private Sound heartbeatSfx;

	// Use this for initialization
	void Start () {
        timer = GetComponent<Timer>();
        base.SetButton(GetComponent<RectTransform>());
	}
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            base.UpdateCircBarUI();
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
                //Pass Out
                hasPassedOut = true;
                heartbeatSfx.source.Stop();
                start = false;
            }
        }
    }

    public void StartBreathingSystem()
    {
        timer.SetDuration(timerDuration);
        timer.StartTimer();

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
}
