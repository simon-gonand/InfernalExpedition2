using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoatPresets", menuName = "Presets/Boat", order = 1)]
public class BoatPresets : ScriptableObject
{
    [Header("Rotation")]
    public float steerSpeed;

    [Header("Speed")]
    public float minSpeed;
    public float maxSpeed;
}
