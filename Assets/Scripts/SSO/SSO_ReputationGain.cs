using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReputationGain", menuName = "Data/SSO/ReputationGain")]
public class SSO_ReputationGain : ScriptableObject
{
    [Range(0, 100)]
    public int ReputationGain;
}
