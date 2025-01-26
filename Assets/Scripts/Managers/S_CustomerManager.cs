using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class S_CustomerManager : MonoBehaviour
{
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

    Customer CurrentCustomer;

    public int itemId;

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        _rseOnItemGive.action += TcheckItemIdGive;

        _ssoSpritesCustomersList.ClearDictionnary();

        _ssoSpritesCustomersList.Setup();

        _rseOnClientLeave.action += CreateCustomer;
        _rseOnTimerEnd.action += TimerEnd;

        CreateCustomer();
    }

    private void OnDestroy()
    {
        _ssoSpritesCustomersList.ClearDictionnary();
        _rseOnItemGive.action -= TcheckItemIdGive;
        _rseOnClientLeave.action -= CreateCustomer;
        _rseOnTimerEnd.action -= TimerEnd;
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

        customer.ItemWanted = itemWanted;
        itemId = itemWanted.Id;
        customer.SpritesDict = spritesDict;

        CurrentCustomer = customer;

        _rsoCurrentCustomers.CurrentCustomer = customer;
        _rseOnClientCreate.RaiseEvent(customer);
    }

    void TcheckItemIdGive(int id)
    {
        if(CurrentCustomer.ItemWanted.Id == id)
        {
            _rseOnGoodArticleGive.RaiseEvent();

            _rseAddReputation.RaiseEvent(_rsoReputationGain.ReputationGain);

            _rseOnCustomerStateChange.RaiseEvent(CustomerState.Happy);

            _rseAddScore.RaiseEvent(10);

            StartCoroutine(SpawnDelay());

        }
        else
        {
            _rseOnBadArticleGive.RaiseEvent();

            _rseOnRemoveReputation.RaiseEvent(_rsoReputationLost.ReputationLost);

            _rseOnCustomerStateChange.RaiseEvent(CustomerState.Angry);

            StartCoroutine(SpawnDelay());

        }

        _rseStopTimer.RaiseEvent();
    }

    void TimerEnd()
    {
        _rseOnRemoveReputation.RaiseEvent(_rsoReputationLost.ReputationLost);
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);

        CreateCustomer();

        yield return null;
    }


}
