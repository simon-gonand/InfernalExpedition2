using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterInBoat : MonoBehaviour
{
    [SerializeField]
    private Transform playerOnBoatEntryPoint;

    private bool isPlayerClimbingInBoat = false;
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerClimbingInBoat) {
            player = other.GetComponent<PlayerController>();
            if (!player.isOnBoat)
            {
                player.self.position = playerOnBoatEntryPoint.position;
            }

            player.isOnBoat = !player.isOnBoat;
        }
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
