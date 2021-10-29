using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailManagement : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform snapPoint;

    private float speed = 0.0f;

    public void InteractWith(PlayerController player)
    {
        player.isInteracting = true;

        Vector3 newPlayerPosition = snapPoint.position;
        newPlayerPosition.y += player.self.lossyScale.y / 2;
        player.self.position = newPlayerPosition;
        player.self.forward = snapPoint.forward;
    }

    public void UninteractWith(PlayerController player)
    {
        player.isInteracting = false;

        speed = 0.0f;
    }

    public void OnAction()
    {
        // No Action possible
    }

    public void OnMove(Vector2 movements)
    {
        speed = movements.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BoatMovements.instance.SetVelocity(speed);
    }
}
