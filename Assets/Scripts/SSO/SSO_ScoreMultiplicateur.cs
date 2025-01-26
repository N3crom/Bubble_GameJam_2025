using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct TimeMultiplicateur
{
    [Range(0, 101)] public int PourcentageTimeMinInclusive;
    
    [Range(0, 101)] public int PourcentageTimeMaxExclusive;
    public float Multiplicateur;
}

[Serializable]
public struct ReputationMultiplicateur
{
    [Range(0, 101)] public int PourcentageReputationMinInclusive;
    [Range(0, 101)] public int PourcentageReputationMaxExclusive;
    public float Multiplicateur;
}


[CreateAssetMenu(fileName = "ScoreMultiplicateur", menuName = "Data/SSO/ScoreMultiplicateur")]
public class SSO_ScoreMultiplicateur : ScriptableObject
{
    public List<TimeMultiplicateur> TimeMultiplicateursList = new List<TimeMultiplicateur>();

    public List<ReputationMultiplicateur> ReputationMultiplicateursList = new List<ReputationMultiplicateur>();
}
