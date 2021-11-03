using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmManagement : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Transform snapPoint;

    private float steer = 0.0f;
    private float initRotationZ;

    // Set the rotation of the helm to the initial rotation
    private void InitRotation()
    {
        Vector3 newRotation = self.rotation.eulerAngles;
        newRotation.z = initRotationZ;
        self.rotation = Quaternion.Euler(newRotation);
    }

    // When player is interacting with the helm
    public void InteractWith(PlayerController player)
    {
        // Get the initial rotation of the helm
        initRotationZ = self.rotation.eulerAngles.z;

        player.isInteracting = true;

        // Snap player to the helm
        Vector3 newPlayerPosition = snapPoint.position;
        newPlayerPosition.y += player.self.lossyScale.y / 2;
        player.self.position = newPlayerPosition;
        player.self.forward = snapPoint.forward;
    }

    // When player is not interacting with the helm anymore
    public void UninteractWith(PlayerController player)
    {
        player.isInteracting = false;

        InitRotation();
        steer = 0.0f; // The player can't turn anymore
    }

    // When the player pressed the action button when he's on the helm
    // No action possible
    public void OnAction()
    {
    }

    // When the player is moving when he's on the helm
    public void OnMove(Vector2 movements)
    {
        // Update steer value
        steer = movements.x;

        // Set the rotation of the helm according to the steering
        if (movements.x == 0)
            InitRotation();
        if (movements.x > 0 && self.rotation.z != 45.0)
        {
            InitRotation();
            self.Rotate(new Vector3(0.0f, 0.0f, -45.0f));
        }
        if (movements.x < 0 && self.rotation.z != 135.0f)
        {
            InitRotation();
            self.Rotate(new Vector3(0.0f, 0.0f, 45.0f));
        }
    }

    private void FixedUpdate()
    {
        // Steer the boat
        BoatMovements.instance.Steer(steer);
    }
}
