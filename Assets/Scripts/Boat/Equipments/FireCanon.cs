using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCanon : MonoBehaviour, IInteractable
{
    public GameObject ball;
    [SerializeField]
    private CanonPresets canonPreset;
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Transform snapPoint;
    [SerializeField]
    private Transform ballSpawnPoint;

    private float nextFire;

    // When the player is interacting with the canon
    public void InteractWith(PlayerController player)
    {
        player.isInteracting = true;

        // Snap player to the canon
        Vector3 newPlayerPosition = snapPoint.position;
        newPlayerPosition.y += player.self.lossyScale.y / 2;
        player.self.position = newPlayerPosition;
        player.self.forward = snapPoint.forward;
    }

    // When the player is not interacting with the canon anymore
    public void UninteractWith(PlayerController player)
    {
        player.isInteracting = false;
    }

    // When the player is pressing the action button when he's on the canon
    public void OnAction()
    {
        Fire();
    }

    // When the player is moving when he's on the canon
    public void OnMove(Vector2 movements)
    {
        // Turn canon ?
    } 

    private void Fire()
    {
        // Check cooldown if the player can shoot again
        if (Time.time > nextFire)
        {
            // Instantiate canon ball
            GameObject ballClone = Instantiate(ball, ballSpawnPoint.position, self.rotation);
            Rigidbody rb = ballClone.AddComponent<Rigidbody>();
            rb.AddForce(ballSpawnPoint.forward * canonPreset.fireSpeed, ForceMode.Impulse);

            // Set that this canon shot the canon ball
            ballClone.GetComponent<CanonBallLifeSpan>().SetCanonWhichFired(self);

            // Reset cooldown
            nextFire = Time.time + canonPreset.fireRate;
        }
    }
}
