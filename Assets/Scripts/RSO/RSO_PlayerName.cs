using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerName", menuName = "Data/RSO/PlayerName")]
public class RSO_PlayerName : ScriptableObject
{
    public string PlayerName;

    public void SetName(string name)
    {
        PlayerName = name;
    }
}
