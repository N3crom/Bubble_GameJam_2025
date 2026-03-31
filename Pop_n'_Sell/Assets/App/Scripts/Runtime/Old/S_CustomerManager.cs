using System.Collections;
using UnityEngine;

public class S_CustomerManager : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] float _shakeDuration;
    [SerializeField] float _shakeMagnitude;
    [SerializeField] float _timeLeave;
    [SerializeField] float _multiplicateurTimePurcentage;
    [SerializeField] float _minimumTimeImpatientValue;
    [SerializeField] int _defaultScoreAdd;

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


    S_ClassCustomer CurrentCustomer;

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
        _rseOnGoodArticleGive.action += ReduceImpatienteTime;

        _ssoSpritesCustomersList.ClearDictionnary();

        _ssoSpritesCustomersList.Setup();

        _rseOnTimerEnd.action += TimerEnd;

        StartCoroutine(FirstSpawnStart());

    }

    private void OnDestroy()
    {
        _ssoSpritesCustomersList.ClearDictionnary();
        _rseOnItemGive.action -= TcheckItemIdGive;
        _rseOnGoodArticleGive.action -= ReduceImpatienteTime;

        _rseOnTimerEnd.action -= TimerEnd;

        _rsoImpatientTime.ImpatientTime = _initialImpactientTimeValue;
    }


    void CreateCustomer()
    {
        var customer = new S_ClassCustomer();
        var itemWanted = _rsoItemsList.Value[Random.Range(0, _rsoItemsList.Value.Count)];
        var spritesDict = _ssoSpritesCustomersList.ListCustomers[Random.Range(0, _ssoSpritesCustomersList.ListCustomers.Count)];
        var enumLenght = System.Enum.GetValues(typeof(S_EnumCustomerState)).Length;
        var randomIndex = Random.Range(0, enumLenght);

        if (System.Enum.IsDefined(typeof(S_EnumCustomerState), randomIndex))
        {
            var customerState = (S_EnumCustomerState)randomIndex;
            customer.CustomerState = customerState;
        }
        customer.CustomerState = S_EnumCustomerState.Neutral;
        customer.ItemWanted = itemWanted;
        itemId = itemWanted.Id;
        customer.SpritesDict = spritesDict;

        CurrentCustomer = customer;

        _rsoCurrentCustomers.Value = customer;
        _rseOnClientCreate.Call(customer);

        _audioSource.volume = 0.05f;
        _audioSource.PlayOneShot(_audioClipBellRing);
        //_audioSource.volume = 1f;

    }

    void TcheckItemIdGive(int id)
    {
        if (CurrentCustomer.ItemWanted.Id == id)
        {
            _audioSource.volume = 1f;

            _audioSource.PlayOneShot(_audioClipGoodCards);

            _rseOnGoodArticleGive.Call();

            _rseAddReputation.Call(_rsoReputationGain.ReputationGain);

            _rseOnCustomerStateChange.Call(S_EnumCustomerState.Happy);

            _rseAddScore.Call(_defaultScoreAdd);

            StartCoroutine(SpawnDelay());

        }
        else
        {
            _audioSource.volume = 0.3f;

            _audioSource.PlayOneShot(_audioClipWrongCards);

            _rseOnBadArticleGive.Call();

            _rseOnRemoveReputation.Call(_rsoReputationLost.ReputationLost);

            _rseOnCustomerStateChange.Call(S_EnumCustomerState.Angry);

            _rseOnCustomerShake.Call(_shakeDuration, _shakeMagnitude);

            StartCoroutine(SpawnDelay());

        }

        _rseStopTimer.Call();
    }

    void TimerEnd()
    {
        _rseOnRemoveReputation.Call(_rsoReputationLost.ReputationLost);

        _rseStopTimer.Call();

        _rseOnBadArticleGive.Call();
        _rseOnCustomerShake.Call(_shakeDuration, _shakeMagnitude);


        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);

        _rseOnClientGoToRight.Call(_timeLeave);

        _rseOnClientLeave.Call();

        yield return new WaitForSeconds(_timeLeave / 2);
        //_rsoImpatientTime.ImpatientTime -= _rsoImpatientTime.ImpatientTime * _multiplicateurTimePurcentage;
        //Debug.Log($"Value before clamp: {_rsoImpatientTime.ImpatientTime}");

        _rsoImpatientTime.ImpatientTime = Mathf.Clamp(_rsoImpatientTime.ImpatientTime, _minimumTimeImpatientValue, 10);
        //Debug.Log($" Value after clamp: {_rsoImpatientTime.ImpatientTime}");
        CreateCustomer();

        yield return null;
    }

    void ReduceImpatienteTime()
    {
        _rsoImpatientTime.ImpatientTime -= _rsoImpatientTime.ImpatientTime * _multiplicateurTimePurcentage;
    }


    IEnumerator FirstSpawnStart()
    {
        yield return new WaitForEndOfFrame();

        CreateCustomer();
    }
}