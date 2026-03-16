using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CustomerState
{
    Neutral,
    Happy,
    Angry
}
public class Customer
{
    public Item ItemWanted;
    public Dictionary<CustomerState, Sprite> SpritesDict;
    public CustomerState CustomerState;
}
