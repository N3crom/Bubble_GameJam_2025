using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Customer CurrentCustomer;

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        _rseOnItemGive.action += TcheckItemIdGive;

        _ssoSpritesCustomersList.ClearDictionnary();

        _ssoSpritesCustomersList.Setup();
    }

    private void OnDestroy()
    {
        _ssoSpritesCustomersList.ClearDictionnary();
        _rseOnItemGive.action -= TcheckItemIdGive;
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
        else
        {
        }

        customer.ItemWanted = itemWanted;
        customer.SpritesDict = spritesDict;

        CurrentCustomer = customer;
        _rsoCurrentCustomers.CurrentCustomer = customer;
        _rseOnClientCreate.RaiseEvent(customer);
    }

    void TcheckItemIdGive(Item item)
    {
        if(CurrentCustomer.ItemWanted.Id == item.Id)
        {
            _rseOnGoodArticleGive.RaiseEvent();
        }
        else
        {
            _rseOnBadArticleGive.RaiseEvent();
        }
    }


}
