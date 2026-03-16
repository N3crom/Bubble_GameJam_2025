using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberItemsDisplay", menuName = "Data/RSO/NumberItemsDisplay")]
public class RSO_NumberItemsDisplay : ScriptableObject
{
    [Range(1, 50)] public int NumberItemsToDisplay;
}
