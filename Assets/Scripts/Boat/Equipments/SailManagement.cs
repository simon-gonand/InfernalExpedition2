using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailManagement : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform snapPoint;

    private float speed = 0.0f;

    // When the player is interacting with the sail
    public void InteractWith(PlayerController player)
    {
        player.isInteracting = true;

        // Snap the player to the sail
        Vector3 newPlayerPosition = snapPoint.position;
        newPlayerPosition.y = player.self.position.y;
        player.self.position = newPlayerPosition;
        player.self.forward = snapPoint.forward;
    }

    // When the player is not interacting with the sail anymore
    public void UninteractWith(PlayerController player)
    {
        player.isInteracting = false;

        // Boat can't increase or decrease speed anymore
        speed = 0.0f;
    }

    // When the player pressed the action button when he's on the sail
    // No Action possible
    public void OnAction()
    {
    }

    // When the player is moving when he's on the sail
    public void OnMove(Vector2 movements)
    {
        // Update speed value
        speed = movements.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Set the velocity of the boat
        BoatMovements.instance.SetVelocity(speed);
    }
}
