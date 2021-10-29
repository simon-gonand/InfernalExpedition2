using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBoatZone : MonoBehaviour
{
    [SerializeField]
    private Transform boat;
    [SerializeField]
    private Transform self;
    [SerializeField]
    private BoxCollider selfCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {          
            Transform player = other.transform;
            player.SetParent(self);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

            if (player.position.y - player.lossyScale.y < selfCollider.size.y + selfCollider.center.y)
            {
                Vector3 position = player.position;
                position.y += selfCollider.size.y;
                player.position = position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
