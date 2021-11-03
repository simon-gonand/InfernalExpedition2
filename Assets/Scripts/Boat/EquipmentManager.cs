using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Empty object that is containing all of the object that are on the boat
public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    private Transform self;

    // Update is called once per frame
    void Update()
    {
        // Update position of all the objects
        self.position = BoatMovements.instance.self.position;
        self.rotation = BoatMovements.instance.self.rotation;
    }
}
