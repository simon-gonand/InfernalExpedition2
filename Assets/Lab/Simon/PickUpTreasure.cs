using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTreasure : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform self;

    public void InteractWith(PlayerController player)
    {
        player.isCarrying = true;
        self.position = player.carryingSnapPoint.position;
        self.SetParent(player.self);
    }

    public void OnAction()
    {
        // Launch
    }

    public void OnMove(Vector2 movements)
    {
        // Nothing on move
    }

    public void UninteractWith(PlayerController player)
    {
        player.isCarrying = false;
        self.SetParent(null);
        //Debug.Log("saucisse");
        StartCoroutine(ApplyGravity());
    }

    IEnumerator ApplyGravity()
    {
        while (!Physics.Raycast(self.position, -Vector3.up, 0.1f)) {
            self.Translate(new Vector3(0.0f, -3.0f, 0.0f) * Time.deltaTime);
        }
        yield return null;
    }
}
