using UnityEngine;

public class ClampingTrigger : MonoBehaviour {

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerMain playerMain;

    public static ClampingTrigger instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("WARNING! More than one instance of ClampingTrigger is created.");
            return;
        }
        instance = this;
        movement = GetComponent<PlayerMovement>();
        playerMain = GetComponent<PlayerMain>();
    }

    public void ActivateLocking() { movement.Lock(); }	

    public void DeactivateLocking() { movement.Unlock(); }

    public void InputSet(bool state) { playerMain.SetInput(state); }

    public void MovementSet(bool state) { playerMain.SetMovement(state); }
}
