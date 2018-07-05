using UnityEngine;

[CreateAssetMenu(fileName = "New Preset", menuName = "Custom/Preset")]
public class Preset : ScriptableObject
{
    public string charName = "default";

    // PlayerVitals
    public float staminaLimit = 100f;
    public float minStamina = -10f;
    public float staminaDrain = 0.125f;
    public float staminaIncrease = 0.04f;

    // PlayerMovement
    public float mouseSensitivity = 2.5f;
    public float runningSpeed = 6f;
    public float walkingSpeed = 3f;
    public float distancePerStep = 100f;

    // HeadBobbing
    public float bobbingSpeed = 0.3f;
    public float bobbingAmount = 0.015f;
    public float sidewaysBobbingAmount = 0.01f;
    public float midPoint = 2.5f;
    public float shakyRun = 3.5f;
}
