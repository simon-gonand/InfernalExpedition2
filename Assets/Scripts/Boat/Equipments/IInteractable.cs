using UnityEngine;

public interface IInteractable
{
    public void InteractWith(PlayerController player);
    public void UninteractWith(PlayerController player);

    public void OnAction();
    public void OnMove(Vector2 movements);
}
