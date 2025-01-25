using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CustomerState
{
    Normal,
    Happy,
    Angry
}
public class Customer
{
    public Item ItemWanted;
    public List<Sprite> SpriteList;
    public CustomerState CustomerState;
}
