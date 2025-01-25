using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Item
{
    public int Id;
    public string Name;
    public List<string> Descriptions;
    public Sprite Sprite;
}
