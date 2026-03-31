using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reputation", menuName = "Data/RSO/ReputationCurrency")]
public class RSO_Reputation : ScriptableObject
{
    [Range(0, 100)]
    public int ReputationCurrency;
}
