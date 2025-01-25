using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkTime", menuName = "Data/RSO/TalkTime")]
public class RSO_TalkTime : ScriptableObject
{
    [Range(0, 1)]
    public float TalkTime;
}
