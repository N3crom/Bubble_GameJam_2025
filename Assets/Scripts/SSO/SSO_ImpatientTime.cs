using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImpatientTime", menuName = "Data/SSO/ImpatientTime")]
public class SSO_ImpatientTime : ScriptableObject
{
    [Range(1, 60)]
    public float ImpatientTime;
}
