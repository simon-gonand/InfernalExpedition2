using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detecting if an object is entering on the boat
public class OnBoatZone : MonoBehaviour
{
    [SerializeField]
    private Transform self;
    [SerializeField]
    private BoxCollider selfCollider;

    private void OnTriggerEnter(Collider other)
    {
        // If the player is entering on the boat
        if (other.CompareTag("Player"))
        {
            Transform player = other.transform;
            // Set the zone as parent of the player to move with it
            player.SetParent(self);
            
            // Update the constraints of the rigid body to avoid gravity
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | 
                RigidbodyConstraints.FreezePositionY;

            // Snap the y position of the player to be sure that the player is not inside but on the boat
            if (player.position.y - player.lossyScale.y < selfCollider.size.y + selfCollider.center.y)
            {
                Vector3 position = player.position;
                position.y += selfCollider.size.y;
                player.position = position;
            }
        }
        // If a treasure is getting on the boat
        else if (other.CompareTag("Treasures"))
        {
            // Just set the zone as parent of the treasure to move with it
            other.transform.SetParent(self);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player is getting off the boat
        if (other.CompareTag("Player"))
        {
            // Remove the parent
            other.transform.SetParent(null);

            // Update rigidbody constraints to apply gravity
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        // If a treasure is getting off the boat
        else if (other.CompareTag("Treasures"))
            // Just remove the parent
            other.transform.SetParent(null);
    }
}
