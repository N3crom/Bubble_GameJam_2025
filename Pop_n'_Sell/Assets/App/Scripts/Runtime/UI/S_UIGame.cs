using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class S_UIGame : MonoBehaviour
{
    [TabGroup("References")]
    [Title("Game Items")]
    [SerializeField] private Transform itemsListParent;

    [TabGroup("References")]
    [SerializeField] private S_Card prefabItem;

    [TabGroup("Inputs")]
    [SerializeField] private RSE_OnTimerStart rseOnTimerStart;

    [TabGroup("Inputs")]
    [SerializeField] private RSE_OnTimerEnd rseOnTimerEnd;

    [TabGroup("Inputs")]
    [SerializeField] private RSE_OnGamePause rseOnGamePause;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_CurrentItemsList rsoCurrentItemsList;

    [TabGroup("Outputs")]
    [SerializeField] private SSO_MaxCardsNumber ssoMaxCardsNumber;

    private List<S_Card> cards = new();
    private S_ObjectPool<S_Card> itemsPool = null;

    private void Awake()
    {
        itemsPool = new S_ObjectPool<S_Card>(prefabItem, ssoMaxCardsNumber.Value, itemsListParent);
    }

    private void OnEnable()
    {
        rseOnTimerStart.action += DisplayCards;
        rseOnTimerEnd.action += HideCards;
        rseOnGamePause.action += ResetCards;

        StartCoroutine(S_Utils.Delay(2f, () => DisplayCards()));

        //StartCoroutine(S_Utils.Delay(5f, () => HideCards()));
    }

    private void OnDisable()
    {
        rseOnTimerStart.action -= DisplayCards;
        rseOnTimerEnd.action -= HideCards;
        rseOnGamePause.action -= ResetCards;
    }

    private void DisplayCards()
    {
        cards.Clear();

        for (int i = 0; i < rsoCurrentItemsList.Value.Count; i++)
        {
            cards.Add(itemsPool.Get());
            cards[i].Setup(rsoCurrentItemsList.Value[i]);
        }
    }

    private void HideCards()
    {
        for (int i = 0; i < rsoCurrentItemsList.Value.Count; i++)
        {
            cards[i].ResetCard();
            itemsPool.ReturnToPool(cards[i]);
        }
    }

    private void ResetCards(bool value)
    {
        if (value)
        {
            for (int i = 0; i < rsoCurrentItemsList.Value.Count; i++)
            {
                cards[i].ResetCard();
            }
        }
    }
}