using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImpatientTime", menuName = "Data/RSO/ImpatientTime")]
public class RSO_ImpatientTime : ScriptableObject
{
    [Range(1, 60)]
    public float ImpatientTime;
}
