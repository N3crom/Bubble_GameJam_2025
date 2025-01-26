using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ReputationManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] RSO_Reputation _rsoReputation;
    [SerializeField] RSE_AddReputation _rseAddScore;
    [SerializeField] RSE_RemoveReputation _rseRemoveScore;
    [SerializeField] RSE_OnReputationChanged _rseOnReputationChanged;
    [SerializeField] RSE_OnGameLost _rseOnGameLost;

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
        if(_rsoReputation.ReputationCurrency > 100) _rsoReputation.ReputationCurrency = 100;
        _rseOnReputationChanged.RaiseEvent();
    }

    private void RemoveReputation(int reputationToRemove)
    {
        _rsoReputation.ReputationCurrency -= reputationToRemove;
        _rseOnReputationChanged.RaiseEvent();

        if(_rsoReputation.ReputationCurrency <= 0)
        {
            _rsoReputation.ReputationCurrency = 0;
            _rseOnReputationChanged.RaiseEvent();
            _rseOnGameLost.RaiseEvent();
        }
    }
}
