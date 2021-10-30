using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTreasure : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Transform self;

    public void InteractWith(PlayerController player)
    {
        Debug.Log("saucisse");
        player.isCarrying = true;
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
