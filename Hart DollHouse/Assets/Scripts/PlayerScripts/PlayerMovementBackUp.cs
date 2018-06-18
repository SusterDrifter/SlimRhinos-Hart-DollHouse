using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
/**
 * Manages player movement and perspective.
 */
public class PlayerMovementBackUp : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity = 2.5f;

    [SerializeField] public Vector3 velocity;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float camRotation;

    [SerializeField] private Camera cam;
    [SerializeField] private float camAngleClamp = 70f;
    [SerializeField] private float currentCamAngle = 0f;

    [SerializeField] private float runningSpeed = 10f;
    [SerializeField] private float walkingSpeed = 5f;

    private Rigidbody obj;
    private PlayerVitals vitals;

    void Start()
    {
        obj = GetComponent<Rigidbody>();
        cam = gameObject.GetComponentInChildren<Camera>();
        vitals = GetComponent<PlayerVitals>();
    }

    /**
     * Perform actual movement of the body.
     */
    private void PerformMovement()
    {
        if (this.velocity != Vector3.zero)
        {
            obj.MovePosition(obj.position + this.velocity * Time.fixedDeltaTime);
        }
    }

    /**
     * Perform actual horizontal rotation of the perspective.
     */
    private void PerformHorizontalRotation()
    {
        // Quaternion is Euler angles + imaginary component. Conversion is needed
        obj.MoveRotation(obj.rotation * Quaternion.Euler(this.rotation));
    }

    /**
     * Perform actual horizontal rotation of the perspective.
     */
    private void PerformVerticalRotation()
    {
        if (cam != null)
        {
            currentCamAngle -= this.camRotation;
            currentCamAngle = Mathf.Clamp(currentCamAngle, -camAngleClamp, camAngleClamp);
            cam.transform.localEulerAngles = new Vector3(currentCamAngle, 0f, 0f);
        }
    }

    /** 
     * Manages keyboard input related to movement of the body.
     */
    public void KeyboardMovement()
    {
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
        this.rotation = new Vector3(0f, yRot, 0f) * mouseSensitivity;

        PerformHorizontalRotation(); // Perform horizontal rotation
        PerformVerticalRotation(); // Perform vertical rotation
    }
}
