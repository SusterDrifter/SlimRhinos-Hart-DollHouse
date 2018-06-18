using UnityEngine;

/**
 * Simulate slight change of height of camera when walking
 * and sprinting.
 */ 
public class HeadBobbing : MonoBehaviour
{
    Preset currentPreset;

    [SerializeField] private float bobbingSpeed; // Speed of the wave
    [SerializeField] private float bobbingAmount; // Frequency of the wave
    [SerializeField] private float sidewaysBobbingAmount; // Frequency of second wave
    [SerializeField] private float midpoint; // Resting position of the camera
    [SerializeField] private float shakyRun;

    [SerializeField] private Camera cam;

    private float timer = 0.0f; // Loosely indicates the position of camera in the wave

    private void Start() {
        currentPreset = PlayerPresets.instance.GetPreset();
        bobbingSpeed = currentPreset.bobbingSpeed;
        bobbingAmount = currentPreset.bobbingAmount;
        sidewaysBobbingAmount = currentPreset.sidewaysBobbingAmount;
        midpoint = currentPreset.midPoint;
        shakyRun = currentPreset.shakyRun;

        cam = Camera.main;
    }

    void Update() {

        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
            // If there is no movement in both axis, we reset the wave
            timer = 0.0f;
        } else {
            // Creating the wave value at that time
            waveslice = Mathf.Sin(timer);
            // Increasing the timer
            timer += bobbingSpeed;
            // In the case that it exceeds, we reset it
            if (timer > Mathf.PI * 2) {
                timer -= (Mathf.PI * 2);
            }
        }

        // Current location of the object
        Vector3 currLocation = cam.transform.localPosition;
      
        // If there is movement of object, we change the value of currLocation
        if (waveslice != 0) {
            float translateChange = waveslice * bobbingAmount; // amplifying the wave amplitude
            float sidewaysChange = waveslice * sidewaysBobbingAmount; // amplifying the wave amplitude
            
            if (Input.GetKey(KeyCode.LeftShift)) {
                translateChange *= shakyRun;
                sidewaysChange *= shakyRun;
            }

            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical); // total length of movement
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f); // Limiting the value of totalAxis

            translateChange *= totalAxes; // updating the change that is required
            sidewaysChange *= totalAxes;

            currLocation.y = midpoint + translateChange; // updating the y value of currLocation
            currLocation.x = midpoint + sidewaysChange;
        } else {
            // Else, we reset the location of object to the allocated midpoint
            currLocation.y = midpoint;
            currLocation.x = midpoint;
        }

        // Update the position of object
        cam.transform.localPosition = currLocation;
    }
}
