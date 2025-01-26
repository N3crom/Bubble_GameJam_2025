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
    [SerializeField] RSO_ImpatientTime _rsoImpatientTime;
    [SerializeField] RSO_Reputation _rsoReputation;
    [SerializeField] RSO_CurrentImpatientTime _rsoCurrentImpatientTime;

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
        float impatienceRatio = (float)_rsoCurrentImpatientTime.CurrentImpatientTime / _rsoImpatientTime.ImpatientTime;

        _rsoScore.Score += (int)(scoreAdd + 5f * impatienceRatio);
        _rseOnScoreChanged.RaiseEvent();
    }

    private void RemoveScore(int scoreToRemove)
    {
        _rsoScore.Score -= scoreToRemove;
        if (_rsoScore.Score < 0) _rsoScore.Score = 0;
        _rseOnScoreChanged.RaiseEvent();
    }

}
