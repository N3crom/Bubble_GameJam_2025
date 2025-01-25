using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UIManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] private RSE_CallPause callPause;

    private void OnEnable()
    {
        callPause.action += ShowPause;
    }

    private void OnDisable()
    {
        callPause.action -= ShowPause;
    }

    private void ShowPause()
    {
        
    }
}
