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
    [SerializeField] AudioSource _audioSource;

    [Header("Parameters Time")]
    [SerializeField] private float timeTranslate;

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

    private IEnumerator Transition(int reputationAdd)
    {
        float startReputation = _rsoReputation.ReputationCurrency;
        float targetReputation = Mathf.Clamp(startReputation + reputationAdd, 0, 100);

        float elapsedTime = 0f;

        while (elapsedTime < timeTranslate)
        {
            elapsedTime += Time.deltaTime;

            _rsoReputation.ReputationCurrency = (int)Mathf.Lerp(startReputation, targetReputation, elapsedTime / timeTranslate);

            _rseOnReputationChanged.RaiseEvent();

            yield return null;
        }

        _rsoReputation.ReputationCurrency = (int)targetReputation;
        _rseOnReputationChanged.RaiseEvent();

        if (_rsoReputation.ReputationCurrency <= 0)
        {
            _rsoReputation.ReputationCurrency = 0;
            _rseOnReputationChanged.RaiseEvent();
            _rseOnGameLost.RaiseEvent();
            //_audioSource.Play();
            _audioSource.PlayOneShot(_audioSource.clip);
        }
    }

    private void AddReputation(int reputationAdd)
    {
        StartCoroutine(Transition(reputationAdd));
    }

    private void RemoveReputation(int reputationToRemove)
    {
        StartCoroutine(Transition(-reputationToRemove));
    }
}
