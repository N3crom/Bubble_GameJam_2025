using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrentCustomer", menuName = "Data/RSO/CurrentCustomer")]
public class RSO_CurrentCustomers : ScriptableObject
{
    public Customer CurrentCustomer;
}
