using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform self;
    [SerializeField]
    private Rigidbody selfRigidBody;
    public Transform carryingSnapPoint;
    [SerializeField]
    private PlayerPresets playerPreset;

    [System.NonSerialized]
    public Transform boatTransform;


    private Vector2 playerMovementInput = Vector2.zero;

    private IInteractable interactingWith;

    #region boolean
    private bool _isInteracting = false;
    public bool isInteracting { get { return _isInteracting; } set { _isInteracting = value; } }

    private bool _isCarrying = false;
    public bool isCarrying { get { return _isCarrying; } set { _isCarrying = value; } }
    private TreasuresCategory _treasureCarried;
    public TreasuresCategory treasureCarried { set { _treasureCarried = value; } }

    private bool _isOnBoat = true;
    public bool isOnBoat { get { return _isOnBoat; } set { _isOnBoat = value; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        boatTransform = BoatMovements.instance.self;
    }

    // When the player moves
    public void OnMove(InputAction.CallbackContext context)
    {
        // If the player is interacting with something he can't move
        if (_isInteracting && interactingWith != null)
            interactingWith.OnMove(context.ReadValue<Vector2>());
        else
            playerMovementInput = context.ReadValue<Vector2>();
    }

    // When the player pressed the action button
    public void OnAction(InputAction.CallbackContext context)
    {
        // If the player is interacting with something he can't attack
        if ((_isInteracting || _isCarrying) && context.performed)
            interactingWith.OnAction();
        // else attack on action pressed
    }

    // When the player pressed the dash button
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selfRigidBody.AddForce(self.forward * playerPreset.dashSpeed, ForceMode.Impulse);
        }
    }

    // When the player pressed the interaction button
    public void OnInteraction(InputAction.CallbackContext context)
    {
        // If the player is not interacting with anything or carrying a treasure
        if (!_isInteracting && !_isCarrying && context.performed)
        {
            // Define from where the raycast will start
            Vector3 startRayPos = self.position;
            startRayPos.y -= self.lossyScale.y / 2;

            // If the raycast is encountering an interactable
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Interactable");
            if (Physics.Raycast(startRayPos, self.forward, out hit, playerPreset.interactionDistance, layerMask))
            {
                // Set with which interactable the player is interacting with
                interactingWith = hit.collider.gameObject.GetComponent<IInteractable>();
                interactingWith.InteractWith(this);
            }
        }
        // Else put the treasure down or uninteract with the interactable
        else if ((_isInteracting || _isCarrying) && context.performed)
        {
            interactingWith.UninteractWith(this);
            interactingWith = null;
        }
    }


    // Update movements of the player
    private void PlayerMovement()
    {
        float currentSpeed = playerPreset.playerSpeed;
        // Apply speed malus if the player is carrying an heavy treasure
        if (_isCarrying && _treasureCarried != null)
            currentSpeed -= _treasureCarried.speedMalus;

        // Apply movements
        Vector3 move = new Vector3(playerMovementInput.x, 0.0f, playerMovementInput.y);
        self.Translate(move * Time.deltaTime * currentSpeed, Space.World);

        // Set the rotation of the player according to his movements
        if (move != Vector3.zero)
            self.forward = move;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
}
