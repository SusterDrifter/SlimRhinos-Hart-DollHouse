using UnityEngine;

/**
 * Call StartTimer() to start timer.
 */ 
public class Timer : MonoBehaviour {

    [SerializeField] private float mainTimer = 10f;
    [SerializeField] private float curTimer;

    private bool canDecrease = true;
    private bool isRunning = false;

	void Start () {
        curTimer = mainTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning) {
            if (canDecrease) { curTimer -= Time.deltaTime; }
            if (curTimer <= 0.0f) { canDecrease = false; }
        }
        
	}

    public void ResetTimer() {
        curTimer = mainTimer;
        canDecrease = true;
        isRunning = false;
    }

    public void DecBy(float time) { curTimer -= time; }

    public void IncBy(float time) { curTimer += time; }

    public void StartTimer() { isRunning = true; }

    public void SetDuration(float time) { mainTimer = time; }

    public bool HasRunOut() { return !canDecrease; }

    public float PercentageFromZero() { return 1 - (curTimer / mainTimer); }

    public void RestartTimer()
    {
        ResetTimer();
        StartTimer();
    }
}
