using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CustomerManager : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] float _shakeDuration;
    [SerializeField] float _shakeMagnitude;
    [SerializeField] float _timeLeave;
    [SerializeField] float _multiplicateurTimePurcentage;
    [SerializeField] float _minimumTimeImpatientValue;

    [Header("Reference")]
    [SerializeField] RSO_CurrentCustomers _rsoCurrentCustomers;
    [SerializeField] SSO_ItemsList _rsoItemsList;
    [SerializeField] SSO_SpritesCustomersList _ssoSpritesCustomersList;
    [SerializeField] RSE_OnGoodArticleGive _rseOnGoodArticleGive;
    [SerializeField] RSE_OnBadArticleGive _rseOnBadArticleGive;
    [SerializeField] RSE_OnItemGive _rseOnItemGive;
    [SerializeField] RSE_OnClientCreate _rseOnClientCreate;
    [SerializeField] RSE_OnClientLeave _rseOnClientLeave;
    [SerializeField] RSE_OnTimerEnd _rseOnTimerEnd;
    [SerializeField] RSE_RemoveReputation _rseOnRemoveReputation;
    [SerializeField] RSE_AddReputation _rseAddReputation;
    [SerializeField] RSO_ReputationLost _rsoReputationLost;
    [SerializeField] RSO_ReputationGain _rsoReputationGain;
    [SerializeField] RSE_AddScore _rseAddScore;
    [SerializeField] RSE_StopTimer _rseStopTimer;
    [SerializeField] RSE_OnCustomerStateChange _rseOnCustomerStateChange;
    [SerializeField] RSE_OnCustomerShake _rseOnCustomerShake;
    [SerializeField] RSE_OnClientGoToRight _rseOnClientGoToRight;
    [SerializeField] RSO_ImpatientTime _rsoImpatientTime;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipWrongCards;
    [SerializeField] AudioClip _audioClipGoodCards;
    [SerializeField] AudioClip _audioClipBellRing;


    Customer CurrentCustomer;

    public int itemId;

    float _initialImpactientTimeValue;

    private void Awake()
    {
        _initialImpactientTimeValue = _rsoImpatientTime.ImpatientTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rseOnItemGive.action += TcheckItemIdGive;

        _ssoSpritesCustomersList.ClearDictionnary();

        _ssoSpritesCustomersList.Setup();

        _rseOnTimerEnd.action += TimerEnd;

        StartCoroutine(FirstSpawnStart());

    }

    private void OnDestroy()
    {
        _ssoSpritesCustomersList.ClearDictionnary();
        _rseOnItemGive.action -= TcheckItemIdGive;
        _rseOnTimerEnd.action -= TimerEnd;

        _rsoImpatientTime.ImpatientTime = _initialImpactientTimeValue;
    }


    void CreateCustomer()
    {
        var customer = new Customer();
        var itemWanted = _rsoItemsList.ItemsList[Random.Range(0, _rsoItemsList.ItemsList.Count)];
        var spritesDict = _ssoSpritesCustomersList.ListCustomers[Random.Range(0, _ssoSpritesCustomersList.ListCustomers.Count)];
        var enumLenght = System.Enum.GetValues(typeof(CustomerState)).Length;
        var randomIndex = Random.Range(0, enumLenght);

        if (System.Enum.IsDefined(typeof(CustomerState), randomIndex))
        {
            var customerState = (CustomerState)randomIndex;
            customer.CustomerState = customerState;
        }
        customer.CustomerState = CustomerState.Neutral;
        customer.ItemWanted = itemWanted;
        itemId = itemWanted.Id;
        customer.SpritesDict = spritesDict;

        CurrentCustomer = customer;

        _rsoCurrentCustomers.CurrentCustomer = customer;
        _rseOnClientCreate.RaiseEvent(customer);

        _audioSource.volume = 0.1f;
        _audioSource.PlayOneShot(_audioClipBellRing);
        //_audioSource.volume = 1f;

    }

    void TcheckItemIdGive(int id)
    {
        if(CurrentCustomer.ItemWanted.Id == id)
        {
            _audioSource.volume = 1f;

            _audioSource.PlayOneShot(_audioClipGoodCards);

            _rseOnGoodArticleGive.RaiseEvent();

            _rseAddReputation.RaiseEvent(_rsoReputationGain.ReputationGain);

            _rseOnCustomerStateChange.RaiseEvent(CustomerState.Happy);

            _rseAddScore.RaiseEvent(10);

            StartCoroutine(SpawnDelay());

        }
        else
        {
            _audioSource.volume = 0.5f;

            _audioSource.PlayOneShot(_audioClipWrongCards);

            _rseOnBadArticleGive.RaiseEvent();

            _rseOnRemoveReputation.RaiseEvent(_rsoReputationLost.ReputationLost);

            _rseOnCustomerStateChange.RaiseEvent(CustomerState.Angry);

            _rseOnCustomerShake.RaiseEvent(_shakeDuration, _shakeMagnitude);

            StartCoroutine(SpawnDelay());

        }

        _rseStopTimer.RaiseEvent();
    }

    void TimerEnd()
    {
        _rseOnRemoveReputation.RaiseEvent(_rsoReputationLost.ReputationLost);

        _rseStopTimer.RaiseEvent();

        _rseOnBadArticleGive.RaiseEvent();
        _rseOnCustomerShake.RaiseEvent(_shakeDuration, _shakeMagnitude);


        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);

        _rseOnClientGoToRight.RaiseEvent(_timeLeave);

        _rseOnClientLeave.RaiseEvent();

        yield return new WaitForSeconds(_timeLeave/2);
        _rsoImpatientTime.ImpatientTime -= _rsoImpatientTime.ImpatientTime * _multiplicateurTimePurcentage;
        //Debug.Log($"Value before clamp: {_rsoImpatientTime.ImpatientTime}");

        _rsoImpatientTime.ImpatientTime = Mathf.Clamp(_rsoImpatientTime.ImpatientTime, _minimumTimeImpatientValue, 10);
        //Debug.Log($" Value after clamp: {_rsoImpatientTime.ImpatientTime}");
        CreateCustomer();

        yield return null;
    }


    IEnumerator FirstSpawnStart()
    {
        yield return new WaitForEndOfFrame();

        CreateCustomer();
    }

}
