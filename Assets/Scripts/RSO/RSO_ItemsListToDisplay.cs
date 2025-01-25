using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsListToDisplay", menuName = "Data/RSO/ItemsListToDisplay")]
public class RSO_ItemsListToDisplay : ScriptableObject
{
    public List<Item> ItemsToDisplay = new List<Item>();
}
