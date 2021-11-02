using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Presets/TreasuresCategory", order = 1)]
public class TreasuresCategory : ScriptableObject
{
    public float launchForce;
    public float speedMalus;
}
