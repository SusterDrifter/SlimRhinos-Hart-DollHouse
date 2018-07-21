using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof (PlayerInput), typeof(PlayerVitals))]
[RequireComponent(typeof(PlayerPresets), typeof(HeadBobbing), typeof(ClampingTrigger))]
/**
 * Main class that manages the entirity of player.
 */
public class PlayerMain : MonoBehaviour {

    private PlayerMovement movement;
    private PlayerVitals vitals;
    private PlayerInput input;

    private bool disableInput = false;
    private bool disableMovement = false;

    void Start() {
        movement = GetComponent<PlayerMovement>();
        vitals = GetComponent<PlayerVitals>();
        input = GetComponent<PlayerInput>();
        GameManager.instance.LockCursor();
    }

    void FixedUpdate () {

        if (!disableMovement)
            movement.KeyboardMovement();
        if (!disableInput)
            movement.MouseViewing();

        vitals.ManageVitals();
	}

    private void Update()
    {
        input.MouseInput();
        input.KeyboardInput();
    }

    public void SetInput(bool state)
    {
        disableInput = state;
    }

    public void SetMovement(bool state)
    {
        disableMovement = state;
    }
}
