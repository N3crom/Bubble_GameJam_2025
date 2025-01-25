using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CustomerManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] RSO_CurrentCustomers _rsoCurrentCustomers;
    [SerializeField] SSO_ItemsList _rsoItemsList;
    [SerializeField] SSO_SpritesCustomersList _ssoSpritesCustomersList;

    private void Awake()
    {
        _ssoSpritesCustomersList.Setup();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        _ssoSpritesCustomersList.ClearDictionnary();
    }
    // Update is called once per frame
    void Update()
    {
        
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

        _rsoCurrentCustomers.CurrentCustomer = customer;
    }

}
