using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class S_GameUI : MonoBehaviour
{
    [Header("RSO")]
    [SerializeField] private RSO_CurrentCustomers currentCustomer;

    [SerializeField] private RSO_Score score;

    [SerializeField] private RSO_ImpatientTime impatientTime;

    [SerializeField] private RSO_Reputation reputation;

    [SerializeField] private RSO_TalkTime talkTime;

    [Header("RSE")]
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_OnScoreChanged onScoreChanged;
    [SerializeField] private RSE_OnReputationChanged onReputationChanged;
    [SerializeField] private RSE_OnTimerStart onTimerStart;
    [SerializeField] private RSE_OnTimerEnd onTimerEnd;
    [SerializeField] private RSE_StopTimer stopTimer;

    [Header("References")]
    [SerializeField] private S_UIManager uiManager;

    [SerializeField] private TextMeshProUGUI textScore;

    [SerializeField] private TextMeshProUGUI textimpatientTime;
    [SerializeField] private Slider sliderimpatientTime;

    [SerializeField] private Slider sliderReputation;
    [SerializeField] private TextMeshProUGUI textReputation;

    [SerializeField] private TextMeshProUGUI textdescription;

    private Coroutine coroutineText;
    private Coroutine coroutineTimer;

    private void OnEnable()
    {
        onScoreChanged.action += ScoreChange;
        onReputationChanged.action += ReputationChanged;

        onTimerStart.action += StartTimer;
        stopTimer.action += StopTimer;
    }

    private void OnDisable()
    {
        onScoreChanged.action -= ScoreChange;
        onReputationChanged.action -= ReputationChanged;

        onTimerStart.action -= StartTimer;
        stopTimer.action -= StopTimer;
    }

    private void Start()
    {
        textScore.text = score.Score.ToString();

        textimpatientTime.text = "";
        sliderimpatientTime.value = impatientTime.ImpatientTime;
        sliderimpatientTime.maxValue = impatientTime.ImpatientTime;

        sliderReputation.value = reputation.ReputationCurrency;
        textReputation.text = reputation.ReputationCurrency.ToString() + "%";
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

        float elapsedTime = 0f;

        while (elapsedTime < impatientTime.ImpatientTime)
        {
            elapsedTime += Time.deltaTime;

            textimpatientTime.text = Mathf.Lerp(impatientTime.ImpatientTime, 0f, elapsedTime / impatientTime.ImpatientTime).ToString("F2") + "s";
            sliderimpatientTime.value = Mathf.Lerp(impatientTime.ImpatientTime, 0f, elapsedTime / impatientTime.ImpatientTime);

            yield return null;
        }

        sliderimpatientTime.value = 0;

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
}
