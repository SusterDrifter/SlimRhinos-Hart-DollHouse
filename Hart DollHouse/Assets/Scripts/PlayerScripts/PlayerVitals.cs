using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerVitals : MonoBehaviour {

    PlayerMovement movement;
    Preset currentPreset;

    [SerializeField] private float staminaLimit;
    [SerializeField] private float minStamina;
    [SerializeField] private float staminaDrain;
    [SerializeField] private float staminaIncrease;

    [SerializeField] private Slider staminaSlider;

    void Start () {

        currentPreset = PlayerPresets.instance.GetPreset();

        staminaLimit = currentPreset.staminaLimit;
        minStamina = currentPreset.minStamina;
        staminaDrain = currentPreset.staminaDrain;
        staminaIncrease = currentPreset.staminaIncrease;

        movement = GetComponent<PlayerMovement>();
        staminaSlider = GetComponentInChildren<Slider>();
        staminaSlider.value = staminaLimit;
	}
    
    public void ManageVitals() {

        // Manages stamina of the object
        if (movement.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift)) {
            staminaSlider.value -= Time.fixedDeltaTime * staminaDrain;
        } else {
            staminaSlider.value += Time.fixedDeltaTime * staminaIncrease;
        }
        staminaSlider.value = Mathf.Clamp(staminaSlider.value, minStamina, staminaLimit);
    }

    /**
     * Indicates whether object has the stamina to run or not.
     */ 
    public bool CanRun() {
        return staminaSlider.value > Time.fixedDeltaTime * staminaDrain;
    }
}
