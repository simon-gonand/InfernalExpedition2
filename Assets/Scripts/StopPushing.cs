using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPushing : MonoBehaviour
{
    [SerializeField]
    private GameObject boat;
    [SerializeField]
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Collider playerBlockingCollider = player.GetComponentInChildren<Collider>();
        Collider boatBlockingCollider = boat.GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(playerBlockingCollider, boatBlockingCollider, true);
    }
}
