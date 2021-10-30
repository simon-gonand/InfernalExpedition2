using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTreasure : MonoBehaviour, IInteractable
{
    [SerializeField]
    private float launchForce;

    [SerializeField]
    private Transform self;

    private Rigidbody selfRigidbody;
    private PlayerController playerInteractingWith;

    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(self.position, -Vector3.up, 0.1f))
            Destroy(selfRigidbody);
    }

    public void InteractWith(PlayerController player)
    {
        playerInteractingWith = player;
        player.isCarrying = true;
        self.position = player.carryingSnapPoint.position;
        self.forward = player.transform.forward;
        self.SetParent(player.self);
    }

    public void OnAction()
    {
        // Launch
        self.SetParent(null);
        selfRigidbody = gameObject.AddComponent<Rigidbody>();
        selfRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        selfRigidbody.AddForce((self.forward + self.up) * launchForce, ForceMode.Impulse);
        playerInteractingWith.isCarrying = false;
        playerInteractingWith = null;
    }

    public void OnMove(Vector2 movements)
    {
        // Nothing on move
    }

    public void UninteractWith(PlayerController player)
    {
        player.isCarrying = false;
        playerInteractingWith = null;
        self.SetParent(null);
        selfRigidbody = gameObject.AddComponent<Rigidbody>();
        selfRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
          
    }
}
