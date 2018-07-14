using UnityEngine;

/**
 * Manages player input that is related to interactivity
 * such as picking up object, dragging items, etc.
 */
public class PlayerInput : MonoBehaviour {

    [SerializeField] Interactable objFocus; // Item in focus, if any
    [SerializeField] Camera cam; // Reference to player camera
    Interactable target;
    DiaryManager diary;
    private bool interactCrossOn = false;

	void Start () {
        objFocus = null;
        cam = Camera.main;
	}

    public void MouseInput() {

        target = null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100)) {
            
            if (hit.collider.tag == "Interactable") {
                target = hit.collider.GetComponent<Interactable>();
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance < target.interactRadius)
                {
                    HoverIcon.instance.InteractCrosshair();
                    interactCrossOn = true;
                }   
            } 
        }

        if (target == null && interactCrossOn ) {
            HoverIcon.instance.DefaultCrossHair();
            interactCrossOn = false;
        }

        // If input is left click and no object is in focus
        if (Input.GetMouseButtonDown(0)) {

            if (target != null)
            {
                // Control the object
                Focus(target);
            }
        }

        // If there is object in focus, we can let go of the object by clicking
        // right mouse button
        if (Input.GetMouseButtonUp(1)) {
            // Let go of object
            Defocus();
        }
    }

    public void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (Inventory.instance.Contains(4))
            {
                if (diary == null)
                    diary = MainUIManager.instance.GetDiary();

                if (!diary.isActive)
                {
                    diary.ActivateDiary();
                    ClampingTrigger.instance.ActivateLocking();
                }
                else if (diary.isActive)
                {
                    diary.DeactivateDiary();
                    ClampingTrigger.instance.DeactivateLocking();
                }
            } else {
                MainUIManager.instance.GetNotificationUI().ShowNotification("You do not possess the Diary.");
            }
            
                
        } 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseUIManager.isPaused)
            {
                PauseUIManager.instance.gameObject.SetActive(true);
                PauseUIManager.instance.PauseGame();
            } else
            {
                PauseUIManager.instance.UnpauseGame();
                PauseUIManager.instance.gameObject.SetActive(false);
            }
        }
    }

    public void Defocus() {
        if (objFocus != null)
        {
            objFocus.OnDefocused();
        }
        objFocus = null;
    }

    public void Focus(Interactable target) {
        if (objFocus != target) {
            if (objFocus != null) {
                objFocus.OnDefocused();
            }
            objFocus = target;
        }

        objFocus.OnFocused(transform);
    }
}
