using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class S_ClassItem
{
    public int Id = 0;
    public string Name = "";
    public List<string> Descriptions = new();
    public Sprite Sprite = null;
}