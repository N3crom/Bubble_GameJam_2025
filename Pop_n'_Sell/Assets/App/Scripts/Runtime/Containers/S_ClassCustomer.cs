using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class S_ClassCustomer
{
    public S_ClassItem itemWanted = new();
    public Dictionary<S_EnumCustomerState, Sprite> spritesDict = new();
    public S_EnumCustomerState customerState = S_EnumCustomerState.Neutral;
}