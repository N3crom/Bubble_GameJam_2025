using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class S_ClassCustomer
{
    public S_ClassItem ItemWanted = new();
    public Dictionary<S_EnumCustomerState, Sprite> SpritesDict = new();
    public S_EnumCustomerState CustomerState = S_EnumCustomerState.Neutral;
}