using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_CartesGenerator : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] float _addItemsAfterPourcentageOfStartImpateintTime;
    [SerializeField] int _maxItemsToDisplay;

    [Header("References")]
    [SerializeField] RSO_ItemsListToDisplay _rsoItemsListToDisplay;
    [SerializeField] RSE_OnClientCreate _rseOnClientCreate;
    [SerializeField] RSE_OnTimerStart _rseOnTimerStart;
    [SerializeField] RSE_OnListGenerationFinish _rseOnListGenerationFinish;
    [SerializeField] SSO_ItemsList _ssoItemsList;
    [SerializeField] RSO_NumberItemsDisplay _rsoNumberItemsDisplay;
    [SerializeField] RSO_ImpatientTime _rsoImpatientTime;

    int _numberStartItems;
    float _startImpatientTime;
    private void Start()
    {
        _numberStartItems = _rsoNumberItemsDisplay.NumberItemsToDisplay;
        _startImpatientTime = _rsoImpatientTime.ImpatientTime;
        //_rsoNumberItemsDisplay.NumberItemsToDisplay = 5;
        _rseOnClientCreate.action += GenerateCards;
        _rseOnTimerStart.action += SpawnCards;

    }

    private void OnDestroy()
    {
        _rsoNumberItemsDisplay.NumberItemsToDisplay = _numberStartItems;

        _rseOnClientCreate.action -= GenerateCards;
        _rseOnTimerStart.action -= SpawnCards;

    }
    void GenerateCards(Customer currentCustomer)
    {

        _rsoItemsListToDisplay.ItemsToDisplay.Clear();

        float currentTimeMax = _rsoImpatientTime.ImpatientTime;
        _rsoNumberItemsDisplay.NumberItemsToDisplay = _numberStartItems + _maxItemsToDisplay - Mathf.CeilToInt(currentTimeMax / _startImpatientTime  * 100 * _addItemsAfterPourcentageOfStartImpateintTime);
        _rsoNumberItemsDisplay.NumberItemsToDisplay = Mathf.Clamp(_rsoNumberItemsDisplay.NumberItemsToDisplay, _numberStartItems, _maxItemsToDisplay);
        //Debug.Log(Mathf.RoundToInt(_startImpatientTime / currentTimeMax * 100 * _addItemsAfterPourcentageOfStartImpateintTime));
        if (_ssoItemsList.ItemsList.Count < _rsoNumberItemsDisplay.NumberItemsToDisplay)
        {
            Debug.LogError("La liste source ne contient pas assez d'éléments pour en sélectionner ");
            return;
        }

        _rsoItemsListToDisplay.ItemsToDisplay.Add(_ssoItemsList.ItemsList.FirstOrDefault(item => item.Id == currentCustomer.ItemWanted.Id));

        int numberCards = 1;

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

        ShuffleList(_rsoItemsListToDisplay.ItemsToDisplay);
    }

    void SpawnCards()
    {
        _rseOnListGenerationFinish.RaiseEvent(_rsoItemsListToDisplay.ItemsToDisplay);

        _rsoNumberItemsDisplay.NumberItemsToDisplay = _numberStartItems;
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
