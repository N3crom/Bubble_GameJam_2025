using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] SSO_ScoreMultiplicateur _ssoScoreMultiplicateur;

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

        int pourcentageTime = Mathf.RoundToInt(_rsoCurrentImpatientTime.CurrentImpatientTime / _rsoImpatientTime.ImpatientTime * 100);

        _rsoScore.Score += Mathf.RoundToInt(scoreAdd * GetMultiplicateurForTime(pourcentageTime, _ssoScoreMultiplicateur.TimeMultiplicateursList) * GetMultiplicateurForReputation(_rsoReputation.ReputationCurrency, _ssoScoreMultiplicateur.ReputationMultiplicateursList));
        _rseOnScoreChanged.RaiseEvent();
    }

    public float GetMultiplicateurForTime(int pourcentageTime, List<TimeMultiplicateur> timesMultiplicateurList)
    {
        foreach (var timeMultiplicateur in timesMultiplicateurList)
        {
            if (pourcentageTime >= timeMultiplicateur.PourcentageTimeMinInclusive &&
                pourcentageTime < timeMultiplicateur.PourcentageTimeMaxExclusive)
            {
                return timeMultiplicateur.Multiplicateur;
            }
        }

        return 1f;
    }
    public float GetMultiplicateurForReputation(int reputation, List<ReputationMultiplicateur> reputationsMultiplicateurList)
    {
        foreach (var reputationMultiplicateur in reputationsMultiplicateurList)
        {
            if (reputation >= reputationMultiplicateur.PourcentageReputationMinInclusive &&
                reputation < reputationMultiplicateur.PourcentageReputationMaxExclusive)
            {
                return reputationMultiplicateur.Multiplicateur;
            }
        }

        return 1f;
    }

    private void RemoveScore(int scoreToRemove)
    {
        _rsoScore.Score -= scoreToRemove;
        if (_rsoScore.Score < 0) _rsoScore.Score = 0;
        _rseOnScoreChanged.RaiseEvent();
    }

}
