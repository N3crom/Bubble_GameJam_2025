using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ReputationManager : MonoBehaviour
{
    [Header("Reference")]
    RSO_Reputation _rsoReputation;
    RSE_AddReputation _rseAddScore;
    RSE_RemoveReputation _rseRemoveScore;
    RSE_OnScoreChanged _rseOnScoreChanged;
    RSE_OnGameLost _rseOnGameLost;

    int _startReputation = 0;
    void Start()
    {
        _startReputation = _rsoReputation.ReputationCurrency;
        _rseAddScore.action += AddReputation;
        _rseRemoveScore.action += RemoveReputation;
    }

    private void OnDestroy()
    {
        _rsoReputation.ReputationCurrency = _startReputation;
        _rseAddScore.action -= AddReputation;
        _rseRemoveScore.action -= RemoveReputation;
    }

    private void AddReputation(int reputationAdd)
    {
        _rsoReputation.ReputationCurrency += reputationAdd;
        _rseOnScoreChanged.RaiseEvent();
    }

    private void RemoveReputation(int reputationToRemove)
    {
        _rsoReputation.ReputationCurrency -= reputationToRemove;
        _rseOnScoreChanged.RaiseEvent();

        if(_rsoReputation.ReputationCurrency <= 0)
        {
            _rseOnGameLost.RaiseEvent();
        }
    }
}
