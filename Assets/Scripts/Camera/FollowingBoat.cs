using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBoat : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    private Vector3 initialOffset;
    private Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialOffset = self.position - BoatMovements.instance.self.position;
        initialPos = self.position;
    }

    // Update is called once per frame
    void Update()
    {
        self.position = new Vector3(initialPos.x, initialPos.y, BoatMovements.instance.self.position.z + initialOffset.z) ;
    }
}
