using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReputationLost", menuName = "Data/RSO/ReputationLost")]
public class RSO_ReputationLost : ScriptableObject
{
    [Range(0, 100)]
    public int ReputationLost;
}
