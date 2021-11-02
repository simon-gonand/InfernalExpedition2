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
    [SerializeField]
    private float dashSpeed;

    [System.NonSerialized]
    public Transform boatTransform;

    [Header("Self References")]
    public Transform self;
    [SerializeField]
    private Rigidbody selfRigidBody;
    public Transform carryingSnapPoint;


    private Vector2 playerMovementInput = Vector2.zero;

    private IInteractable interactingWith;
    private bool _isInteracting = false;
    public bool isInteracting { get { return _isInteracting; } set { _isInteracting = value; } }

    private bool _isCarrying = false;
    public bool isCarrying { get { return _isCarrying; } set { _isCarrying = value; } }
    private TreasuresCategory _treasureCarried;
    public TreasuresCategory treasureCarried { set { _treasureCarried = value; } }

    private bool _isOnBoat = true;
    public bool isOnBoat { get { return _isOnBoat; } set { _isOnBoat = value; } }

    // Start is called before the first frame update
    void Start()
    {
        boatTransform = BoatMovements.instance.self;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_isInteracting && interactingWith != null)
            interactingWith.OnMove(context.ReadValue<Vector2>());
        else
            playerMovementInput = context.ReadValue<Vector2>();
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if ((_isInteracting || _isCarrying) && context.performed)
            interactingWith.OnAction();
        // else attack on action pressed
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selfRigidBody.AddForce(self.forward * dashSpeed, ForceMode.Impulse);
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!_isInteracting && !_isCarrying && context.performed)
        {
            Vector3 startRayPos = self.position;
            startRayPos.y -= self.lossyScale.y / 2;

            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Interactable");
            if (Physics.Raycast(startRayPos, self.forward, out hit, interactionDistance, layerMask))
            {
                interactingWith = hit.collider.gameObject.GetComponent<IInteractable>();
                interactingWith.InteractWith(this);
            }
        }
        else if ((_isInteracting || _isCarrying) && context.performed)
        {
            interactingWith.UninteractWith(this);
            interactingWith = null;
        }
    }

    private void PlayerMovement()
    {
        float currentSpeed = playerSpeed;
        if (_isCarrying && _treasureCarried != null)
            currentSpeed -= _treasureCarried.speedMalus;
        Vector3 move = new Vector3(playerMovementInput.x, 0.0f, playerMovementInput.y);
        self.Translate(move * Time.deltaTime * currentSpeed, Space.World);
        if (move != Vector3.zero)
            self.forward = move;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
}
