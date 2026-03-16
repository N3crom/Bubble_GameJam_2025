using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsList", menuName = "Data/SSO/ItemsList")]
public class SSO_ItemsList : ScriptableObject
{
    public List<Item> ItemsList;
}
