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

    void Start() {
        movement = GetComponent<PlayerMovement>();
        vitals = GetComponent<PlayerVitals>();
        input = GetComponent<PlayerInput>();
    }

    void FixedUpdate () {
        movement.KeyboardMovement();
        movement.MouseViewing();
        vitals.ManageVitals();
	}

    private void Update()
    {
        input.MouseInput();
        input.KeyboardInput();
    }
}
