using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3f;

    private InputAction interactAction;

    private int layerMask;

    void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        interactAction.started += ctx => SendInteractionRay();
        interactAction.Enable();

        layerMask = LayerMask.GetMask("Interactable");
    }

    void SendInteractionRay()
    {
        Ray interactionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(interactionRay, out RaycastHit hitInfo, interactionRange, layerMask))
        {
            Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
            interactable?.Interact();
        }
    }
}
