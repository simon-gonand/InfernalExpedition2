using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Rigidbody selfRigidbody;
    public TreasuresCategory category;

    private PlayerController playerInteractingWith;

    public void InteractWith(PlayerController player)
    {
        playerInteractingWith = player;
        player.isCarrying = true;
        player.treasureCarried = category;
        self.position = player.carryingSnapPoint.position;
        self.forward = player.transform.forward;
        self.SetParent(player.self);
    }

    public void OnAction()
    {
        // Launch
        self.SetParent(null);
        selfRigidbody.isKinematic = false;
        selfRigidbody.AddForce((self.forward + self.up) * category.launchForce, ForceMode.Impulse);
        playerInteractingWith.isCarrying = false;
        playerInteractingWith.treasureCarried = null;
        playerInteractingWith = null;
    }

    public void OnMove(Vector2 movements)
    {
        // Nothing on move
    }

    public void UninteractWith(PlayerController player)
    {
        player.isCarrying = false;
        player.treasureCarried = null;
        playerInteractingWith = null;
        self.SetParent(null);
        selfRigidbody.isKinematic = false;
    }

    private void Update()
    {
        Vector3 raycastStartPos = self.position;
        raycastStartPos.y -= self.lossyScale.y / 2;
        if (Physics.Raycast(raycastStartPos, -Vector3.up, 0.03f))
        {
            selfRigidbody.isKinematic = true;
        }
    }
}
