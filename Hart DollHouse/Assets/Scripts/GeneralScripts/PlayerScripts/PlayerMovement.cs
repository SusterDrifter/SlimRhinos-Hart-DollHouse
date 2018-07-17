using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
/**
 * Manages player movement and perspective,
 * as well takes care of the calculation of foodstep sounds.
 */
public class PlayerMovement : MonoBehaviour
{
    Preset currentPreset;

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float walkingSpeed;

    public Vector3 velocity;
    private float rotation;
    private float camRotation;

    private float originalRotation;
    private float originalCamRotation;

    private Camera cam;
    [SerializeField] private float minCamAngleClamp = -70f;
    [SerializeField] private float maxCamAngleClamp = 70f;

    private float minRotationClamp = -float.PositiveInfinity;
    private float maxRotationClamp = float.PositiveInfinity;

    private float currentCamAngle = 0f;
    private float currentRotation = 0f;

    [SerializeField] private float rotationLock = 20f;
    [SerializeField] private float camRotationLock = 20f;

    private bool movementLock = false;
    private float camAngleClampNorm = 70f;
    private float rotationClampNorm = float.PositiveInfinity;

    private Rigidbody obj;
    private PlayerVitals vitals;

    private Sound[] footSteps;
    private Sound[] stoppingSteps;
    [SerializeField] private float distanceTraveled = 0f;
    [SerializeField] private float distancePerStep;
    private int soundIndex;
    private int stoppingIndex;
    private bool needStoppingSound = false;

    void Start()
    {
        currentPreset = PlayerPresets.instance.GetPreset();
        mouseSensitivity = currentPreset.mouseSensitivity;
        runningSpeed = currentPreset.runningSpeed;
        walkingSpeed = currentPreset.walkingSpeed;
        distancePerStep = currentPreset.distancePerStep;

        obj = GetComponent<Rigidbody>();
        cam = Camera.main;
        vitals = GetComponent<PlayerVitals>();

        footSteps = new Sound[4];
        stoppingSteps = new Sound[2];
    }

    private void Update()
    {
        CheckFootsteps();
    }

    /**
     * Perform actual movement of the body.
     */
    private void PerformMovement()
    {
        if (this.velocity != Vector3.zero)
        {
            needStoppingSound = true;
            distanceTraveled += this.velocity.magnitude;
            obj.MovePosition(obj.position + this.velocity * Time.fixedDeltaTime);
        } else {
            distanceTraveled = 0f;
            if (needStoppingSound)
                StopFootsteps();
        }
    }

    /**
     * Perform actual horizontal rotation of the perspective.
     */
    private void PerformHorizontalRotation()
    {
        currentRotation += this.rotation;
        currentRotation = Mathf.Clamp(currentRotation, minRotationClamp, maxRotationClamp);
        // Quaternion is Euler angles + imaginary component. Conversion is needed
        obj.MoveRotation(Quaternion.Euler(new Vector3(0f, currentRotation, 0f)));
    }

    /**
     * Perform actual horizontal rotation of the perspective.
     */
    private void PerformVerticalRotation()
    {
        if (cam != null)
        {
            currentCamAngle -= this.camRotation;
            currentCamAngle = Mathf.Clamp(currentCamAngle, minCamAngleClamp, maxCamAngleClamp);
            cam.transform.localEulerAngles = new Vector3(currentCamAngle, 0f, 0f);
        }
    }

    /** 
     * Manages keyboard input related to movement of the body.
     */
    public void KeyboardMovement()
    {
        if (!movementLock) {
            // Takes in keyboard input 
            float zKey = Input.GetAxisRaw("Horizontal");
            float xKey = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * zKey;
            Vector3 moveVertical = transform.forward * xKey;

            // Velocity vector that the character is moving to
            this.velocity = (moveHorizontal + moveVertical).normalized;

            // Checks whether sprint button is pressed and manage accordingly
            if (Input.GetKey(KeyCode.LeftShift) && vitals.CanRun())
            {
                this.velocity *= runningSpeed;
            }
            else
            {
                this.velocity *= walkingSpeed;
            }

            // Perform movement of the body
            PerformMovement();
        }
    }

    /**
     * Manages cursor input to rotate the object's perspective
     */
    public void MouseViewing()
    {
        // Horizontal rotation of the body
        float yRot = Input.GetAxisRaw("Mouse X"); // Getting the input
        // Vertical rotation of the camera
        float xRot = Input.GetAxisRaw("Mouse Y"); // Getting the input

        this.camRotation = xRot * mouseSensitivity;
        this.rotation = yRot * mouseSensitivity;

        PerformHorizontalRotation(); // Perform horizontal rotation
        PerformVerticalRotation(); // Perform vertical rotation
    }

    public void Lock() {
        minCamAngleClamp = currentCamAngle - camRotationLock;
        maxCamAngleClamp = currentCamAngle + camRotationLock;

        minRotationClamp = currentRotation - rotationLock;
        maxRotationClamp = currentRotation + rotationLock;

        movementLock = true;
    }

    public void Unlock() {
        minCamAngleClamp = -camAngleClampNorm;
        maxCamAngleClamp = camAngleClampNorm;

        minRotationClamp = -rotationClampNorm;
        maxRotationClamp = rotationClampNorm;
        movementLock = false;
    }

    private void CheckFootsteps()
    {
        if (distanceTraveled > distancePerStep) {
            distanceTraveled %= distancePerStep;
            if (footSteps[soundIndex] == null)
                footSteps[soundIndex] = AudioManager.instance.GetSound(Sound.SoundType.SoundEffect, "Footstep" + soundIndex);
            AudioManager.instance.PlayClip(footSteps[soundIndex]);
            soundIndex++;
            soundIndex %= footSteps.Length;
        }
    }

    private void StopFootsteps()
    {
        stoppingIndex = (soundIndex + 1) % 2;
        if (stoppingSteps[stoppingIndex] == null) {
            stoppingSteps[stoppingIndex] = AudioManager.instance.
                GetSound(Sound.SoundType.SoundEffect, "Stopstep" + stoppingIndex);
        }
        AudioManager.instance.PlayClip(stoppingSteps[stoppingIndex]);
        soundIndex++;
        soundIndex %= footSteps.Length;
        needStoppingSound = false;
    }
}
