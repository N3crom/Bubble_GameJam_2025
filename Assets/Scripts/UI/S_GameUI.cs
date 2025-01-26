using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class S_GameUI : MonoBehaviour
{
    [Header("RSO")]
    [SerializeField] private RSO_CurrentCustomers currentCustomer;

    [SerializeField] private RSO_Score score;

    [SerializeField] private RSO_ImpatientTime impatientTime;
    [SerializeField] private RSO_CurrentImpatientTime currentImpatientTime;

    [SerializeField] private RSO_Reputation reputation;

    [SerializeField] private RSO_TalkTime talkTime;

    [Header("RSE")]
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_OnScoreChanged onScoreChanged;
    [SerializeField] private RSE_OnReputationChanged onReputationChanged;
    [SerializeField] private RSE_OnTimerStart onTimerStart;
    [SerializeField] private RSE_OnTimerEnd onTimerEnd;
    [SerializeField] private RSE_StopTimer stopTimer;
    [SerializeField] private RSE_OnListGenerationFinish onListGenerationFinish;
    [SerializeField] private RSE_OnBadArticleGive onBadArticleGive;
    [SerializeField] private RSE_OnGoodArticleGive onGoodArticleGive;
    [SerializeField] private RSE_OnClientLeave onClientLeave;

    [Header("References")]
    [SerializeField] private S_UIManager uiManager;

    [SerializeField] private TextMeshProUGUI textScore;

    [SerializeField] private TextMeshProUGUI textimpatientTime;
    [SerializeField] private Slider sliderimpatientTime;

    [SerializeField] private Slider sliderReputation;
    [SerializeField] private TextMeshProUGUI textReputation;

    [SerializeField] private TextMeshProUGUI textdescription;

    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private List<string> listTextGood;
    [SerializeField] private List<string> listTextBad;

    private Coroutine coroutineText;
    private Coroutine coroutineTimer;

    private void OnEnable()
    {
        onScoreChanged.action += ScoreChange;
        onReputationChanged.action += ReputationChanged;

        onTimerStart.action += StartTimer;
        stopTimer.action += StopTimer;

        onListGenerationFinish.action += InitialisationCarts;

        onGoodArticleGive.action += BubbleSucces;
        onBadArticleGive.action += BubbleFail;

        onClientLeave.action += RemoveBubble;
    }

    private void OnDisable()
    {
        onScoreChanged.action -= ScoreChange;
        onReputationChanged.action -= ReputationChanged;

        onTimerStart.action -= StartTimer;
        stopTimer.action -= StopTimer;

        onListGenerationFinish.action -= InitialisationCarts;

        onGoodArticleGive.action -= BubbleSucces;
        onBadArticleGive.action -= BubbleFail;

        onClientLeave.action -= RemoveBubble;
    }

    private void Start()
    {
        textScore.text = score.Score.ToString();

        textimpatientTime.text = "";
        sliderimpatientTime.value = impatientTime.ImpatientTime;
        sliderimpatientTime.maxValue = impatientTime.ImpatientTime;

        sliderReputation.value = reputation.ReputationCurrency;
        textReputation.text = reputation.ReputationCurrency.ToString() + "%";

        textdescription.text = "";
    }

    private IEnumerator TextDisplay(string text)
    {
        textdescription.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            textdescription.text += text[i];

            yield return new WaitForSeconds(talkTime.TalkTime);
        }
    }

    private IEnumerator SliderTime()
    {
        sliderimpatientTime.maxValue = impatientTime.ImpatientTime;
        currentImpatientTime.CurrentImpatientTime = impatientTime.ImpatientTime;

        float elapsedTime = 0f;

        while (elapsedTime < impatientTime.ImpatientTime)
        {
            elapsedTime += Time.deltaTime;

            textimpatientTime.text = Mathf.Lerp(impatientTime.ImpatientTime, 0f, elapsedTime / impatientTime.ImpatientTime).ToString("F2") + "s";
            sliderimpatientTime.value = Mathf.Lerp(impatientTime.ImpatientTime, 0f, elapsedTime / impatientTime.ImpatientTime);

            currentImpatientTime.CurrentImpatientTime = Mathf.Lerp(impatientTime.ImpatientTime, 0f, elapsedTime / impatientTime.ImpatientTime);

            yield return null;
        }

        sliderimpatientTime.value = 0;
        currentImpatientTime.CurrentImpatientTime = 0;

        onTimerEnd.RaiseEvent();

        textimpatientTime.text = "";
        sliderimpatientTime.value = impatientTime.ImpatientTime;
        sliderimpatientTime.maxValue = impatientTime.ImpatientTime;
    }

    private void StartTimer()
    {
        textdescription.transform.parent.gameObject.SetActive(true);

        int index = Random.Range(0, currentCustomer.CurrentCustomer.ItemWanted.Descriptions.Count);

        string text = currentCustomer.CurrentCustomer.ItemWanted.Descriptions[index];

        coroutineText = StartCoroutine(TextDisplay(text));

        coroutineTimer = StartCoroutine(SliderTime());
    }

    private void StopTimer()
    {
        if(coroutineText != null)
        {
            StopCoroutine(coroutineText);
        }
        
        if(coroutineTimer != null)
        {
            StopCoroutine(coroutineTimer);
        }

        textimpatientTime.text = "";
        sliderimpatientTime.value = impatientTime.ImpatientTime;
        sliderimpatientTime.maxValue = impatientTime.ImpatientTime;

        for (int i = gridParent.childCount - 1; i >= 0; i--)
        {
            Destroy(gridParent.GetChild(i).gameObject);
        }
    }

    private void InitialisationCarts(List<Item> listItems)
    {
        for (int i = 0; i < listItems.Count; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, gridParent);

            newItem.GetComponent<S_Carte>().dataValue = listItems[i].Id;
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = listItems[i].Sprite;
        }
    }

    private void ScoreChange()
    {
        textScore.text = score.Score.ToString();
    }

    private void ReputationChanged()
    {
        sliderReputation.value = reputation.ReputationCurrency;
        textReputation.text = reputation.ReputationCurrency.ToString() + "%";
    }

    private void RemoveBubble()
    {
        textdescription.text = "";

        textdescription.transform.parent.gameObject.SetActive(false);
    }

    private void BubbleSucces()
    {
        int random = Random.Range(0, listTextGood.Count);

        textdescription.text = listTextGood[random];
    }

    private void BubbleFail()
    {
        int random = Random.Range(0, listTextBad.Count);

        textdescription.text = listTextBad[random];
    }
}
