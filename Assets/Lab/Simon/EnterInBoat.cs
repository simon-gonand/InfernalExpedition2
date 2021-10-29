using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                isPlayerClimbingInBoat = true;
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
        if (isPlayerClimbingInBoat)
        {
            Vector3 updateOnZ = player.self.position;
            updateOnZ.z = playerOnBoatEntryPoint.position.z;
            player.self.position = updateOnZ;
            if (player.self.position.y != playerOnBoatEntryPoint.position.y)
            {
                Vector3 moveOnY = player.self.position;
                moveOnY.y = Mathf.Lerp(player.self.position.y, playerOnBoatEntryPoint.position.y, 2.0f * Time.deltaTime);
                player.self.position = moveOnY;
            }
            else
            {
                Vector3 moveOnX = player.self.position;
                moveOnX.x = Mathf.Lerp(player.self.position.x, playerOnBoatEntryPoint.position.x, 2.0f * Time.deltaTime);
                player.self.position = moveOnX;
            }
        }
        if (player != null && player.self.position.x == playerOnBoatEntryPoint.position.x && 
            player.self.position.y == playerOnBoatEntryPoint.position.y)
        {
            Debug.Log("saucisse");
            isPlayerClimbingInBoat = false;
        }
    }
}
