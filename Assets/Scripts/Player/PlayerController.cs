using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Header("Stat")]
    private float playerSpeed;
    [SerializeField]
    [Range(0,2)]private float interactionDistance;

    [System.NonSerialized]
    public Transform boatTransform;

    [Header("Self References")]
    public Transform self;


    private Vector2 playerMovementInput = Vector2.zero;

    private IInteractable interactingWith;
    private bool _isInteracting = false;
    public bool isInteracting { get { return _isInteracting; } set { _isInteracting = value; } }

    private bool _isOnBoat = true;
    public bool isOnBoat { get { return _isOnBoat; } set { _isOnBoat = value; } }

    // Start is called before the first frame update
    void Start()
    {
        boatTransform = BoatMovements.instance.self;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isInteracting)
            playerMovementInput = context.ReadValue<Vector2>();
        else
            interactingWith.OnMove(context.ReadValue<Vector2>());
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (isInteracting && context.performed)
            interactingWith.OnAction();
        // else attack on action pressed
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        //Debug.Log(context.performed);
        if (!isInteracting && context.performed)
        {
            Vector3 startRayPos = self.position;
            startRayPos.y -= self.lossyScale.y / 2;

            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Equipment");
            if (Physics.Raycast(startRayPos, self.forward, out hit, interactionDistance, layerMask))
            {
                interactingWith = hit.collider.gameObject.GetComponent<IInteractable>();
                interactingWith.InteractWith(this);
            }
        }
        else if (isInteracting && context.performed)
        {
            interactingWith.UninteractWith(this);
        }
    }

    private void PlayerMovement()
    {
        Vector3 move = new Vector3(playerMovementInput.x, 0.0f, playerMovementInput.y);
        self.Translate(move * Time.deltaTime * playerSpeed, Space.World);
        if (move != Vector3.zero)
            self.forward = move;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInteracting)
            PlayerMovement();
    }
}
