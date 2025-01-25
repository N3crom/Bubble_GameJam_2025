using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReputationLost", menuName = "Data/SSO/ReputationLost")]
public class SSO_ReputationLost : ScriptableObject
{
    [Range(0, 100)]
    public int ReputationLost;
}
