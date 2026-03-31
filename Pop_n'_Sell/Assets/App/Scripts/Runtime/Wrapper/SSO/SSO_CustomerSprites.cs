using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyValuePair
{
    public S_EnumCustomerState Key;
    public Sprite Value;
}

[CreateAssetMenu(fileName = "CustomerSprites", menuName = "Data/SSO/CustomerSprites")]
public class SSO_CustomerSprites : ScriptableObject
{
    [SerializeField]private List<KeyValuePair> CustomerSprites = new List<KeyValuePair>();
    public string NameCustomers;
    public Dictionary<S_EnumCustomerState, Sprite> SpritesCustomersDictionnary = new Dictionary<S_EnumCustomerState, Sprite>();

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
