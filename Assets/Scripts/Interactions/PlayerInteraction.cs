/*

using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void StopInteraction();
}

public class PlayerInteraction : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;
    private bool isInteracting;
    private IInteractable currentInteractable;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    currentInteractable = interactObj;
                    isInteracting = true;
                    currentInteractable.Interact();
                }
            }
        } else if (isInteracting && Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            isInteracting = false;
            currentInteractable.StopInteraction();
        }
    }
}

*/


using UnityEngine;

interface IInteractable
{
    void Interact();
    void StopInteraction();
}

public class PlayerInteraction : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;
    private bool isInteracting;
    private IInteractable currentInteractable;

    void Start()
    {
        // Initialisierung, falls notwendig
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isInteracting)
        {
            Ray ray = new Ray(interactorSource.position, interactorSource.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    currentInteractable = interactObj;
                    isInteracting = true;
                    currentInteractable.Interact();
                }
            }
        }
        else if (isInteracting && Input.GetKeyDown(KeyCode.Mouse0) && currentInteractable != null)
        {
            isInteracting = false;
            currentInteractable.StopInteraction();
        }
    }

    public bool IsInteracting()
    {
        return isInteracting;
    }
}
