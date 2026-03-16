using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyValuePair
{
    public CustomerState Key;
    public Sprite Value;
}

[CreateAssetMenu(fileName = "CustomerSprites", menuName = "Data/SSO/CustomerSprites")]
public class SSO_CustomerSprites : ScriptableObject
{
    [SerializeField]private List<KeyValuePair> CustomerSprites = new List<KeyValuePair>();
    public string NameCustomers;
    public Dictionary<CustomerState, Sprite> SpritesCustomersDictionnary = new Dictionary<CustomerState, Sprite>();

    public void InitializeDictionnary()
    {
        foreach (KeyValuePair kvp in CustomerSprites)
        {
            if (SpritesCustomersDictionnary.ContainsKey(kvp.Key) == false) {

                SpritesCustomersDictionnary.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
