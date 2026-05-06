using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private PlayerInput controls;
    public float interactionRange = 3f; 
    private string actionState = ""; 
    private Vector3 pickplace = Vector3.zero;
    void Awake() 
    { 
        controls = GetComponent<PlayerInput>();
        if (controls != null) {
            Debug.Log("I have controls"); 
        }
        else 
        {
            Debug.Log("I have NO controls"); 
        } 
    }

    void Update()// furue me update. Used with Unity 6000 input controls check player input controler for each action setup
    { 
        if (controls != null) 
        { 
            if (controls.actions["push"].triggered) 
            { 
                actionState = "Pushing"; 
                Debug.Log("I have been pushed up"); 
                TryInteract(); 
            } 
            if (controls.actions["pull"].triggered) 
            {
                actionState = "Pulling";
                Debug.Log("I have been pulled up");
                TryInteract(); 
            }
            if (controls.actions["hold"].IsPressed()) 
            { actionState = "Holding";
                Debug.Log("I am holding");
                TryInteract(); 
            } 
        } else 
        { Debug.Log("I have no control up"); 
        } 
    }
    void TryInteract()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, interactionRange))
        {

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            RotateButton Button = hit.collider.GetComponent<RotateButton>();
            Vector3 rayDirection = ray.direction;
            pickplace = this.transform.position + rayDirection;
            if (interactable != null)
            {
                interactable.Interact(hit.point, hit.normal, pickplace, actionState);
            }
            if (Button != null)
            {
                Button.Interact(hit.point, hit.normal, pickplace, actionState);
            }
        }
    }
}