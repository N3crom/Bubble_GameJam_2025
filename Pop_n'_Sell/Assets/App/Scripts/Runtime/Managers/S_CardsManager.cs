using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_CardsManager : MonoBehaviour
{
    [TabGroup("Inputs")]
    [SerializeField] private RSE_OnClientCreate rseOnClientCreate;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_CurrentItemsList rsoCurrentItemsList;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_CurrentCardsNumber rsoCurrentCardsNumber;

    [TabGroup("Outputs")]
    [SerializeField] private RSO_CurrentImpatientTime rsoCurrentImpatientTime;

    [TabGroup("Outputs")]
    [SerializeField] private SSO_ItemsList ssoItemList;

    [TabGroup("Outputs")]
    [SerializeField] private SSO_StartCardsNumber ssoStartCardsNumber;

    [TabGroup("Outputs")]
    [SerializeField] private SSO_MaxCardsNumber ssoMaxCardsNumber;

    [TabGroup("Outputs")]
    [SerializeField] private SSO_StartImpatientTime ssoStartImpatientTime;

    [TabGroup("Outputs")]
    [SerializeField] private SSO_MinImpatientTime ssoMinImpatientTime;

    private void OnEnable()
    {
        rseOnClientCreate.action += GenerateCards;

        S_ClassCustomer custom = new S_ClassCustomer()
        {
            itemWanted = ssoItemList.Value[Random.Range(0, ssoItemList.Value.Count)],
            customerState = S_EnumCustomerState.Neutral,
            spritesDict = new Dictionary<S_EnumCustomerState, Sprite>()
        };

        GenerateCards(custom);
    }

    private void OnDisable()
    {
        rseOnClientCreate.action -= GenerateCards;

        rsoCurrentItemsList.Value.Clear();
        rsoCurrentCardsNumber.Value = ssoStartCardsNumber.Value;
        rsoCurrentImpatientTime.Value.time = ssoStartImpatientTime.Value.time;
    }

    private void GenerateCards(S_ClassCustomer currentCustomer)
    {
        rsoCurrentItemsList.Value.Clear();

        int number = ssoStartCardsNumber.Value + ssoMaxCardsNumber.Value - Mathf.CeilToInt(rsoCurrentImpatientTime.Value.time / ssoStartImpatientTime.Value.time * 100 * 0.1f);
        number = Mathf.Clamp(number, ssoStartCardsNumber.Value, ssoMaxCardsNumber.Value);
        rsoCurrentCardsNumber.Value = number;

        rsoCurrentItemsList.Value = ssoItemList.Value.Where(item => item.id != currentCustomer.itemWanted.id).OrderBy(x => Random.value).Take(number - 1).Append(currentCustomer.itemWanted).OrderBy(x => Random.value).ToList();
    }
}