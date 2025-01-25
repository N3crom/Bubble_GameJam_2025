using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "SpritesCustomersList", menuName = "Data/SSO/SpritesCustomersList")]
public class SSO_SpritesCustomersList : ScriptableObject
{
    [SerializeField]private List<SSO_CustomerSprites> SpritesCustomersList;


    public List<Dictionary<CustomerState, Sprite>> ListCustomers;

    public void Setup()
    {
        foreach (var sprites in SpritesCustomersList)
        {
            sprites.InitializeDictionnary();
            ListCustomers.Add(sprites.SpritesCustomersDictionnary);
        }
    }

    public void ClearDictionnary()
    {
        foreach (var sprites in SpritesCustomersList)
        {
            sprites.SpritesCustomersDictionnary.Clear();          
        }
    }

}


