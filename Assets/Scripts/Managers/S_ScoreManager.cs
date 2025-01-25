using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreManager : MonoBehaviour
{
    //[Header("Settings")]
    [Header("Reference")]
    [SerializeField] RSO_Score _rsoScore;
    [SerializeField] RSE_AddScore _rseAddScore;
    [SerializeField] RSE_OnScoreChanged _rseOnScoreChanged;

    void Start()
    {
        _rseAddScore.action += AddScore;
        
    }

    private void OnDestroy()
    {
        _rsoScore.Score = 0;
        _rseAddScore.action -= AddScore;

    }

    private void AddScore(int scoreAdd)
    {
        _rsoScore.Score += scoreAdd;
        _rseOnScoreChanged.RaiseEvent();
    }

    private void RemoveScore(int scoreToRemove)
    {
        _rsoScore.Score -= scoreToRemove;
        if (_rsoScore.Score < 0) _rsoScore.Score = 0;
        _rseOnScoreChanged.RaiseEvent();
    }

}
