using UnityEngine;

public class ClampingTrigger : MonoBehaviour {

    [SerializeField] private PlayerMovement movement;

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
    }

    public void ActivateLocking() { movement.Lock(); }	

    public void DeactivateLocking() { movement.Unlock(); }
}
