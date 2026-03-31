using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReputationGain", menuName = "Data/RSO/ReputationGain")]
public class RSO_ReputationGain : ScriptableObject
{
    [Range(0, 100)]
    public int ReputationGain;
}
