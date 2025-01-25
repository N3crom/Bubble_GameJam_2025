using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CartesGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RSO_ItemsListToDisplay _rsoItemsListToDisplay;
    [SerializeField] RSE_OnClientCreate _rseOnClientCreate;
    [SerializeField] RSE_OnListGenerationFinish _rseOnListGenerationFinish;
    [SerializeField] SSO_ItemsList _ssoItemsList;
    [SerializeField] RSO_NumberItemsDisplay _rsoNumberItemsDisplay;

    int _numberItems;
    private void Start()
    {
        _numberItems = _rsoNumberItemsDisplay.NumberItemsToDisplay;

        _rseOnClientCreate.action += GenerateCards;

    }

    private void OnDestroy()
    {
        _rsoNumberItemsDisplay.NumberItemsToDisplay = _numberItems;

        _rseOnClientCreate.action -= GenerateCards;

    }
    void GenerateCards(Customer currentCustomer)
    {

        _rsoItemsListToDisplay.ItemsToDisplay.Clear();


        if (_ssoItemsList.ItemsList.Count < _rsoNumberItemsDisplay.NumberItemsToDisplay)
        {
            Debug.LogError("La liste source ne contient pas assez d'éléments pour en sélectionner ");
            return;
        }

        _rsoItemsListToDisplay.ItemsToDisplay.Add(_ssoItemsList.ItemsList[currentCustomer.ItemWanted.Id]);

        int numberCards = 1;

        ShuffleList(_ssoItemsList.ItemsList);

        foreach (var element in _ssoItemsList.ItemsList)
        {
            if (!_rsoItemsListToDisplay.ItemsToDisplay.Contains(element))
            {
                _rsoItemsListToDisplay.ItemsToDisplay.Add(element);
                numberCards++;

                if (_rsoNumberItemsDisplay.NumberItemsToDisplay == numberCards)
                    break;
            }
        }

        _rseOnListGenerationFinish.RaiseEvent(_rsoItemsListToDisplay.ItemsToDisplay);
    }


    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
