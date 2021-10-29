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

    private void InitRotation()
    {
        Vector3 newRotation = self.rotation.eulerAngles;
        newRotation.z = initRotationZ;
        self.rotation = Quaternion.Euler(newRotation);
    }

    public void InteractWith(PlayerController player)
    {
        initRotationZ = self.rotation.eulerAngles.z;

        player.isInteracting = true;

        Vector3 newPlayerPosition = snapPoint.position;
        newPlayerPosition.y += player.self.lossyScale.y / 2;
        player.self.position = newPlayerPosition;
        player.self.forward = snapPoint.forward;
    }

    public void UninteractWith(PlayerController player)
    {
        player.isInteracting = false;

        InitRotation();
        steer = 0.0f;
    }

    public void OnAction()
    {
        // No action possible
    }

    public void OnMove(Vector2 movements)
    {
        steer = movements.x;
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
        BoatMovements.instance.Steer(steer);
    }
}
